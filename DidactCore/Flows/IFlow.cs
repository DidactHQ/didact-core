using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public interface IFlow
    {
        /// <summary>
        /// Configures the Flow metadata to be saved to persistent storage.
        /// </summary>
        /// <returns></returns>
        Task ConfigureAsync();

        /// <summary>
        /// Asynchronously executes the Flow.
        /// </summary>
        /// <param name="jsonInputString"></param>
        /// <returns></returns>
        Task ExecuteAsync(string? jsonInputString = null);
    }
}
