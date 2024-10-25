using Microsoft.Extensions.DependencyInjection;

namespace DidactCore.Plugins
{
    public interface IPluginDependencyInjectionRegistrar
    {
        /// <summary>
        /// Creates a new <see cref="IServiceCollection"/> containing all dependencies registered in the plugin.
        /// </summary>
        /// <returns></returns>
        IServiceCollection CreateServiceCollection();
    }
}
