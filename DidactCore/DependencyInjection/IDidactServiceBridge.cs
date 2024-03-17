using Microsoft.Extensions.DependencyInjection;

namespace DidactCore.DependencyInjection
{
    public interface IDidactServiceBridge
    {
        /// <summary>
        /// <para>
        ///     Creates a new <see cref="IServiceCollection"/> containing all dependencies used in the Flow Library.
        /// </para>
        /// <para>
        ///     This <see cref="IServiceCollection"/> is dynamically merged with the <see cref="IServiceCollection"/> from Didact Engine.
        /// </para>
        /// </summary>
        /// <returns></returns>
        IServiceCollection CreateServiceCollection();
    }
}
