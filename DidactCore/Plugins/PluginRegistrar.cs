using Microsoft.Extensions.DependencyInjection;

namespace DidactCore.Plugins
{
    public class PluginRegistrar : IPluginRegistrar
    {
        public PluginRegistrar() { }

        public IServiceCollection CreateServiceCollection()
        {
            IServiceCollection pluginServiceCollection = new ServiceCollection();

            // Register services for the Flow Library...
            // ...
            // ...

            return pluginServiceCollection;
        }
    }
}
