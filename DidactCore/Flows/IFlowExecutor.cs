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
        /// <para>
        ///     Retrieves all Flow types from the AppDomain's assemblies using reflection,
        ///     instantiates each Flow type using the dependency injection system,
        ///     and runs their configuration functions.
        /// </para>
        /// <para>
        ///     If configuration fails for a specific set of Flows,
        ///     gracefully handles the failures and passes through the successes.
        /// </para>
        /// </summary>
        /// <returns></returns>
        Task ConfigureFlowsAsync();

        /// <summary>
        /// Asynchronously executes the IFlow instance by running its ExecuteAsync method and utilizing its Flow metadata.
        /// </summary>
        /// <param name="flowRunDto"></param>
        /// <returns></returns>
        Task ExecuteFlowInstanceAsync(FlowRunDto flowRunDto);
    }
}
