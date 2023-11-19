using DidactCore.Models.Flows;
using DidactCore.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DidactCore
{
    private class SomeFlow : IFlow
    {
        private readonly ILogger _logger;
        private readonly IFlowRepository _flowRepository;

        public SomeFlow(ILogger logger, IFlowRepository flowRepository)
        {
            _logger = logger;
            _flowRepository = flowRepository;
        }

        public async Task ConfigureAsync()
        {
            var flowConfigurator = new FlowConfigurator();
            flowConfigurator
                .WithName("A flow name.")
                .WithDescription("A flow description");

            await _flowRepository.SaveConfigurationsAsync(flowConfigurator).ConfigureAwait(false);
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("A test log event from SomeFlow.");
            await Task.CompletedTask;
        }
    }
}
