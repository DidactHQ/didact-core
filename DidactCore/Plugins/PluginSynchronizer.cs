using DidactCore.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidactCore.Plugins
{
    public class PluginSynchronizer
    {
        private readonly IFlowRepository _flowRepository;
        private readonly IPluginContainer _pluginContainer;

        public PluginSynchronizer(IFlowRepository flowRepository, IPluginContainer pluginContainer)
        {
            _flowRepository = flowRepository;
            _pluginContainer = pluginContainer;
        }

        public async Task SynchronizeFlowsAsync()
        {
            var successfulFlowConfigurators = new List<FlowConfiguratorDto>();
            var failedFlowConfigurators = new List<FlowConfiguratorDto>();
            var flowTypesInstantiated = new List<Type>();
            var flowTypesNotInstantiated = new List<Type>();

            var flowConfiguratorsInstantiated = new List<FlowConfiguratorDto>();
            var flowConfiguratorsNotInstantiated = new List<FlowConfiguratorDto>();
            var flowConfiguratorsSynchronized = new { };
            var flowConfiguratorsNotSynchronized = new { };

            var flowTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IFlow)) && t.IsClass && !t.IsAbstract);

            // Configurator part 1: instantiate the Flows.
            foreach (var flowType in flowTypes)
            {
                var iflow = _pluginContainer.PluginDependencyInjector.CreateInstance(flowType) as IFlow;

                if (iflow is null)
                {
                    var exception = new Exception(
                        $"The Flow type {flowType.Name} could not be instantiated with dependency injection during Flow configuration.");
                    flowTypesNotInstantiated.Add(flowType);

                    var failedFlowConfigurator = new FlowConfiguratorDto()
                    {
                        FlowType = flowType,
                        Exception = exception
                    };
                    flowConfiguratorsNotInstantiated.Add(failedFlowConfigurator);

                    continue;
                }

                flowTypesInstantiated.Add(flowType);

                var successfulFlowConfigurator = new FlowConfiguratorDto()
                {
                    FlowType = flowType,
                    FlowInstance = iflow
                };
                flowConfiguratorsInstantiated.Add(successfulFlowConfigurator);
            }

            if (flowConfiguratorsInstantiated.Count == 0)
            {
                // TODO Throw exception for failed plugin instantiations.
            }

            // Configurator part 2: execute the configuration functions.
            foreach (var flowConfiguratorInstantiated in flowConfiguratorsInstantiated)
            {
                try
                {
                    var flowConfigurator = flowConfiguratorInstantiated.FlowInstance!.Configure();
                    await _flowRepository.SaveConfigurationsAsync(flowConfigurator);
                }
                catch (Exception ex)
                {
                    successfulFlowConfigurators.Remove(flowConfigurator);

                    var exception = new Exception(
                        $"The Flow configurator for Flow type {flowConfigurator.FlowType.Name} has failed. See inner exception.", ex);
                    flowConfigurator.Exception = exception;
                    failedFlowConfigurators.Add(flowConfigurator);
                }
            }

            foreach (var flowConfigurator in failedFlowConfigurators)
            {
                // TODO Handle failed flow configurators.
            }

            foreach (var flowConfigurator in successfulFlowConfigurators)
            {
                // TODO Handle successful flow configurators.
            }
        }
    }
}
