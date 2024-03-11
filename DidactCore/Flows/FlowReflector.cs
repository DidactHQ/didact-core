using DidactCore.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public class FlowReflector : IFlowReflector
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFlowRepository _flowRepository;

        public FlowReflector(IServiceProvider serviceProvider, IFlowRepository flowRepository)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _flowRepository = flowRepository ?? throw new ArgumentNullException(nameof(flowRepository));
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
    }
}
