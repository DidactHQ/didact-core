using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public interface IFlowExecutor
    {
        /// <summary>
        /// Retrieves the Flow type from the AppDomain's assemblies using reflection
        /// and instantiates the Flow using the dependency injection system.
        /// </summary>
        /// <param name="flowRunDto"></param>
        /// <returns></returns>
        Task<FlowRunDto> CreateFlowInstanceAsync(FlowRunDto flowRunDto);

        /// <summary>
        /// Asynchronously executes the IFlow instance by running its ExecuteAsync method and utilizing its Flow metadata.
        /// </summary>
        /// <param name="flowRunDto"></param>
        /// <returns></returns>
        Task ExecuteFlowInstanceAsync(FlowRunDto flowRunDto);
    }
}
