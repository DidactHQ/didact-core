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
    }
}
