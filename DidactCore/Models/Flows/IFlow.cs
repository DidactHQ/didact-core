using System.Threading.Tasks;

namespace DidactCore.Models.Flows
{
    public interface IFlow
    {
        /// <summary>
        /// Configures the name, description, queue designations, and other metadata of the Flow.
        /// </summary>
        /// <returns></returns>
        Task ConfigureAsync();

        /// <summary>
        /// Asynchronously executes the Flow.
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();
    }
}
