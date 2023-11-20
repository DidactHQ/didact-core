using DidactCore.Models.Flows;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DidactCore
{
    public class SomeFlow : IFlow
    {
        private readonly ILogger _logger;
        private readonly IFlowConfigurator _flowConfigurator;

        public SomeFlow(ILogger logger, IFlowConfigurator flowConfigurator)
        {
            _logger = logger;
            _flowConfigurator = flowConfigurator;
        }

        public async Task ConfigureAsync()
        {
            _flowConfigurator
                .WithName("A flow name.")
                .WithDescription("A flow description");

            await _flowConfigurator.SaveConfigurationsAsync().ConfigureAwait(false);
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("A test log event from SomeFlow.");
            await Task.CompletedTask;
        }
    }
}
