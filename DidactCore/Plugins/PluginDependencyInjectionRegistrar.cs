using Microsoft.Extensions.DependencyInjection;

namespace DidactCore.Plugins
{
    public class PluginDependencyInjectionRegistrar : IPluginDependencyInjectionRegistrar
    {
        public PluginDependencyInjectionRegistrar() { }

        public IServiceCollection CreateServiceCollection()
        {
            IServiceCollection bridgeServiceCollection = new ServiceCollection();

            // Add services for the Flow Library to the bridgeServiceCollection...
            // ...
            // ...

            return bridgeServiceCollection;
        }
    }
}
