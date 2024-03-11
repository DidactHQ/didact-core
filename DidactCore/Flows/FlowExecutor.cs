using DidactCore.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public class FlowExecutor : IFlowExecutor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFlowRepository _flowRepository;
        private readonly IFlowLogger _flowLogger;

        public FlowExecutor(IServiceProvider serviceProvider, IFlowRepository flowRepository, IFlowLogger flowLogger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _flowRepository = flowRepository ?? throw new ArgumentNullException(nameof(flowRepository));
            _flowLogger = flowLogger ?? throw new ArgumentNullException(nameof(flowLogger));
        }

        public async Task<IFlow> CreateFlowInstanceAsync(Flow flow)
        {
            // Traverse the AppDomain's assemblies to get the type.
            // Remember that .NET 5+ only has 1 AppDomain going forward, so CurrentDomain is sufficient.
            var flowType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes()).Where(t => t.Name == flow.TypeName).SingleOrDefault();

            if (flowType is null)
            {
                await _flowRepository.DeactivateFlowByIdAsync(flow.FlowId);
                throw new ArgumentNullException();
            }

            // Create an instance of the type using the dependency injection system.
            // Then safe cast to an IFlow.
            var iflow = ActivatorUtilities.CreateInstance(_serviceProvider, flowType) as IFlow
                ?? throw new NullReferenceException();

            return iflow;
        }

        public async Task ExecuteFlowInstanceAsync(IFlow flowInstance, Flow flow)
        {
            var flowId = flow.FlowId;
            try
            {
                await flowInstance.ExecuteAsync();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
