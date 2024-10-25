using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public interface IFlow
    {
        /// <summary>
        /// Configures the Flow metadata as a returned <see cref="IFlowConfigurator"/>.
        /// </summary>
        /// <returns></returns>
        IFlowConfigurator Configure();

        /// <summary>
        /// Asynchronously executes the Flow.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ExecuteAsync(IFlowExecutionContext context);
    }
}
