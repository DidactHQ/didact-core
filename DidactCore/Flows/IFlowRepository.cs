using System.Collections.Generic;
using System.Threading.Tasks;

namespace DidactCore.Flows
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

        /// <summary>
        /// Asynchronously retrieves a Flow from persistent storage by its primary key.
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        Task<IFlow> GetFlowByIdAsync(long flowId);

        /// <summary>
        /// Asynchronously retrieves a Flow from persistent storage by its FullyQualifiedType name.
        /// </summary>
        /// <param name="fullyQualifiedTypeName"></param>
        /// <returns></returns>
        Task<IFlow> GetFlowByFullyQualifiedTypeNameAsync(string fullyQualifiedTypeName);

        /// <summary>
        /// Asynchronously retrieves a Flow from persistent storage by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IFlow> GetFlowByNameAsync(string name);

        /// <summary>
        /// Asynchronously retrieves all Flows previously saved to persistent storage.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IFlow>> GetAllFlowsFromStorageAsync();
    }
}
