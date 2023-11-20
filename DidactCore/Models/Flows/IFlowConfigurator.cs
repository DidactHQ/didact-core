using System.Threading.Tasks;

namespace DidactCore.Models.Flows
{
    /// <summary>
    /// <para>A helper interface that configures Flow metadata.</para>
    /// <para>This interface and its implementations need to be registered as a transient dependency in Didact Engine.</para>
    /// </summary>
    public interface IFlowConfigurator
    {
        /// <summary>
        /// The Flow's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The Flow's description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Sets the Flow name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IFlowConfigurator WithName(string name);

        /// <summary>
        /// Sets the Flow description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        IFlowConfigurator WithDescription(string description);

        /// <summary>
        /// Asynchronously saves the Flow configurations to persistent storage.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SaveFlowConfigurationsException"></exception>
        Task SaveConfigurationsAsync();
    }
}
