using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public interface IFlowExecutor
    {
        /// <summary>
        /// Asynchronously retrieves the next available FlowRun from persistent storage.
        /// </summary>
        /// <returns></returns>
        Task<FlowRunDto> FetchFlowRunAsync();

        /// <summary>
        /// Identifies a compatible plugin to instantiate a FlowRun by its Type using the plugin's dependency injection system
        /// and converts it to an <see cref="IFlow"/> instance.
        /// </summary>
        /// <param name="flowRunDto"></param>
        /// <returns></returns>
        Task<FlowRunDto> CreateFlowInstanceAsync(FlowRunDto flowRunDto);

        /// <summary>
        /// Asynchronously executes the FlowRun by running its <see cref="IFlow.ExecuteAsync(IFlowExecutionContext)"/> method.
        /// </summary>
        /// <param name="flowRunDto"></param>
        /// <returns></returns>
        Task ExecuteFlowRunAsync(FlowRunDto flowRunDto);
    }
}
