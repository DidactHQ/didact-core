using DidactCore.Flows;
using System.Threading.Tasks;

namespace DidactCore.Repositories
{
    public interface IFlowRepository
    {
        /// <summary>
        /// Asynchronously saves the values from IFlowConfigurator to persistent storage.
        /// </summary>
        /// <param name="flowConfigurator"></param>
        /// <see cref="IFlowConfigurator"/>
        /// <returns></returns>
        Task SaveConfigurationsAsync(IFlowConfigurator flowConfigurator);
    }
}
