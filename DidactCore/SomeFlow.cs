using DidactCore.Models.Flows;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DidactCore
{
    public class SomeFlow : IFlow
    {
        private readonly ILogger _logger;

        public SomeFlow(ILogger logger)
        {
            _logger = logger;
        }

        public async Task ConfigureAsync(IFlowConfigurator flowConfigurator)
        {
            flowConfigurator
                .WithName("A flow name.")
                .WithDescription("A flow description");

            await flowConfigurator.SaveConfigurationsAsync().ConfigureAwait(false);
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("A test log event from SomeFlow.");
            await Task.CompletedTask;
        }
    }
}
