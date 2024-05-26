using DidactCore.Engine;
using DidactCore.Entities;
using DidactCore.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public class FlowExecutor : IFlowExecutor
    {
        private readonly IEngineSupervisor _engineSupervisor;
        private readonly IFlowRepository _flowRepository;
        private readonly IFlowLogger _flowLogger;

        public FlowExecutor(IEngineSupervisor engineSupervisor, IFlowRepository flowRepository, IFlowLogger flowLogger)
        {
            _engineSupervisor = engineSupervisor ?? throw new ArgumentNullException(nameof(engineSupervisor));
            _flowRepository = flowRepository ?? throw new ArgumentNullException(nameof(flowRepository));
            _flowLogger = flowLogger ?? throw new ArgumentNullException(nameof(flowLogger));
        }

        public async Task<FlowInstanceDto> CreateFlowInstanceAsync(Flow flow)
        {
            IPluginContainer pluginContainer;

            try
            {
                pluginContainer = _engineSupervisor.PluginContainers.FindMatchingPluginContainer(flow.AssemblyFullName, flow.TypeName);
            }
            catch (NoMatchedPluginException ex)
            {
                throw;
            }
            catch (MultipleMatchedPluginsException ex)
            {
                throw;
            }

            // Traverse the AppDomain's assemblies to get the type.
            // Remember that .NET 5+ only has 1 AppDomain going forward, so CurrentDomain is sufficient.
            var flowType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.Name == flow.TypeName && t.GetInterfaces().Contains(typeof(IFlow)) && t.IsClass && !t.IsAbstract)
                .SingleOrDefault();

            if (flowType is null)
            {
                await _flowRepository.DeactivateFlowByIdAsync(flow.FlowId);
                throw new ArgumentNullException();
            }

            // Create an instance of the type using the dependency injection system.
            // Then safe cast to an IFlow.
            var iflow = pluginContainer.PluginDependencyInjector.CreateInstance(flowType) as IFlow
                ?? throw new NullReferenceException();

            var flowInstanceDto = new FlowInstanceDto()
            {
                Flow = flow,
                FlowInstance = iflow
            };

            return flowInstanceDto;
        }

        public async Task ConfigureFlowsAsync()
        {
            var successfulFlowConfigurators = new List<FlowConfiguratorDto>();
            var failedFlowConfigurators = new List<FlowConfiguratorDto>();

            var flowTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IFlow)) && t.IsClass && !t.IsAbstract);

            // Configurator part 1: instantiate the Flows.
            foreach (var flowType in flowTypes)
            {
                var pluginContainer = _engineSupervisor.PluginContainers.FindMatchingPluginContainer(flowType);
                var iflow = pluginContainer.PluginDependencyInjector.CreateInstance(flowType) as IFlow;

                if (iflow is null)
                {
                    var exception = new Exception(
                        $"The Flow type {flowType.Name} could not be instantiated with dependency injection during Flow configuration.");
                    var failedFlowConfigurator = new FlowConfiguratorDto()
                    {
                        FlowType = flowType,
                        Exception = exception
                    };
                    failedFlowConfigurators.Add(failedFlowConfigurator);
                    continue;
                }

                var successfulFlowConfigurator = new FlowConfiguratorDto()
                {
                    FlowType = flowType,
                    FlowInstance = iflow
                };
                successfulFlowConfigurators.Add(successfulFlowConfigurator);
            }

            // Configurator part 2: execute the configuration functions.
            foreach (var flowConfigurator in successfulFlowConfigurators)
            {
                try
                {
                    await flowConfigurator.FlowInstance!.ConfigureAsync();
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

        public async Task ExecuteFlowInstanceAsync(FlowInstanceDto flowInstanceDto)
        {
            var flowId = flowInstanceDto.Flow.FlowId;
            try
            {
                await flowInstanceDto.FlowInstance.ExecuteAsync();
            }
            catch (Exception)
            {
                // TODO
                // Handle retries here.
                // After final failure, log to storage.
                // Do not actually throw;
            }
        }
    }
}
