using Microsoft.Extensions.DependencyInjection;

namespace DidactCore.DependencyInjection
{
    public interface IDidactServiceBridge
    {
        IServiceCollection CreateServiceCollection();
    }
}
