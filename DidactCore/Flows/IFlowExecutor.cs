using DidactCore.Entities;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public interface IFlowExecutor
    {
        /// <summary>
        /// Retrieves the Flow type from the AppDomain's assemblies using reflection
        /// and instantiates the Flow using the dependency injection system.
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        Task<IFlow> CreateFlowInstanceAsync(Flow flow);

        /// <summary>
        /// Asynchronously executes the IFlow instance by running its ExecuteAsync method and utilizing its Flow metadata.
        /// </summary>
        /// <param name="flowInstance"></param>
        /// <param name="flow"></param>
        /// <returns></returns>
        Task ExecuteFlowInstanceAsync(IFlow flowInstance, Flow flow);
    }
}
