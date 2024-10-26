using DidactCore.Engine;
using DidactCore.Plugins;
using System;
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

        public async Task<FlowRunDto> CreateFlowInstanceAsync(FlowRunDto flowRunDto)
        {
            IPluginContainer pluginContainer;

            try
            {
                pluginContainer = _engineSupervisor.PluginContainers.FindMatchingPluginContainer(flowRunDto.PluginExecutionVersion);
            }
            catch (NoMatchedPluginException ex)
            {
                throw;
            }

            // Traverse the AppDomain's assemblies to get the type.
            // Remember that .NET 5+ only has 1 AppDomain going forward, so CurrentDomain is sufficient.
            var flowType = pluginContainer.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.Name == flowRunDto.Flow.TypeName && t.GetInterfaces().Contains(typeof(IFlow)) && t.IsClass && !t.IsAbstract)
                .SingleOrDefault();

            if (flowType is null)
            {
                throw new FlowTypeNotFoundException();
            }

            // Create an instance of the type using the dependency injection system.
            // Then safe cast to an IFlow.
            var iflow = pluginContainer.PluginDependencyInjector.CreateInstance(flowType) as IFlow
                ?? throw new NullReferenceException();

            // I'm going to leave the method signature async for the moment, so fulfill the signature.
            await Task.CompletedTask;

            flowRunDto.FlowInstance = iflow;
            return flowRunDto;
        }

        public async Task ExecuteFlowInstanceAsync(FlowRunDto flowRunDto)
        {
            if (flowRunDto.FlowInstance is null)
            {
                throw new Exception("Oops.");
            }

            try
            {
                await flowRunDto.FlowInstance.ExecuteAsync();
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
