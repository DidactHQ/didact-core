using Microsoft.Extensions.DependencyInjection;

namespace DidactCore.DependencyInjection
{
    public class DidactServiceBridge : IDidactServiceBridge
    {
        public DidactServiceBridge() { }

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
