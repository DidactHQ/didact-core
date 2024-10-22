using DidactCore.Constants;
using DidactCore.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var flowConfigurators = new List<FlowConfiguratorDto>();
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
                    var notInstantiatedFlowConfigurator = new FlowConfiguratorDto()
                    {
                        FlowType = flowType,
                        State = FlowConfiguratorStates.FlowInstantiationFailed,
                        Exception = exception
                    };
                    flowConfigurators.Add(notInstantiatedFlowConfigurator);
                    continue;
                }

                var instantiatedFlowConfigurator = new FlowConfiguratorDto()
                {
                    FlowType = flowType,
                    State = FlowConfiguratorStates.FlowInstantiationSuccessful,
                    FlowInstance = iflow
                };
                flowConfigurators.Add(instantiatedFlowConfigurator);
            }

            // Configurator part 2: execute the configuration functions.
            foreach (var flowConfiguratorDto in flowConfigurators.Where(c => c.State == FlowConfiguratorStates.FlowInstantiationSuccessful))
            {
                try
                {
                    var iFlowConfigurator = flowConfiguratorDto.FlowInstance!.Configure();
                    await _flowRepository.SaveConfigurationsAsync(iFlowConfigurator);
                    flowConfiguratorDto.State = FlowConfiguratorStates.FlowConfigurationSuccessful;
                }
                catch (Exception ex)
                {
                    var exception = new Exception(
                        $"The Flow configurator for Flow type {flowConfiguratorDto.FlowType.Name} has failed. See inner exception.", ex);
                    flowConfiguratorDto.Exception = exception;
                    flowConfiguratorDto.State = FlowConfiguratorStates.FlowConfigurationFailed;
                }
            }

            foreach (var flowConfigurator in flowConfigurators.Where(c => c.State == FlowConfiguratorStates.FlowInstantiationFailed))
            {
                // TODO Handle failed flow instantiations.
            }

            foreach (var flowConfigurator in flowConfigurators.Where(c => c.State == FlowConfiguratorStates.FlowConfigurationFailed))
            {
                // TODO Handle failed flow configurations.
            }

            foreach (var flowConfigurator in flowConfigurators.Where(c => c.State == FlowConfiguratorStates.FlowConfigurationSuccessful))
            {
                // TODO Handle successful flow configurators.
            }
        }
    }
}
