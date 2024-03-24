using DidactCore.DependencyInjection;
using DidactCore.Dtos;
using DidactCore.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public class FlowExecutor : IFlowExecutor
    {
        private readonly IDidactDependencyInjector _didactDependencyInjector;
        private readonly IFlowRepository _flowRepository;
        private readonly IFlowLogger _flowLogger;

        public FlowExecutor(IDidactDependencyInjector didactDependencyInjector, IFlowRepository flowRepository, IFlowLogger flowLogger)
        {
            _didactDependencyInjector = didactDependencyInjector ?? throw new ArgumentNullException(nameof(didactDependencyInjector));
            _flowRepository = flowRepository ?? throw new ArgumentNullException(nameof(flowRepository));
            _flowLogger = flowLogger ?? throw new ArgumentNullException(nameof(flowLogger));
        }

        public async Task<FlowInstanceDto> CreateFlowInstanceAsync(Flow flow)
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
            var iflow = _didactDependencyInjector.CreateInstance(flowType) as IFlow
                ?? throw new NullReferenceException();

            var flowInstanceDto = new FlowInstanceDto()
            {
                Flow = flow,
                FlowInstance = iflow
            };

            return flowInstanceDto;
        }

        public async Task ExecuteFlowInstanceAsync(FlowInstanceDto flowInstanceDto)
        {
            var flowId = flowInstanceDto.Flow.FlowId;
            try
            {
                await flowInstanceDto.FlowInstance.ExecuteAsync();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
