using DidactCore.Models.Blocks;
using DidactCore.Models.Constants;
using DidactCore.Models.Flows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DidactCore
{
    public class SomeFlow : IFlow
    {
        private readonly ILogger _logger;
        private readonly IFlowConfigurator _flowConfigurator;
        private readonly IServiceProvider _serviceProvider;

        public SomeFlow(ILogger logger, IFlowConfigurator flowConfigurator, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _flowConfigurator = flowConfigurator;
            _serviceProvider = serviceProvider;
        }

        public async Task ConfigureAsync()
        {
            _flowConfigurator
                .WithName("SomeFlow")
                .WithDescription("A flow description.")
                .ForQueue(QueueTypes.HyperQueue, "Default");

            await _flowConfigurator.SaveConfigurationsAsync().ConfigureAwait(false);
        }

        public async Task ExecuteAsync(string? jsonInputString)
        {
            var actionBlock = ActivatorUtilities.CreateInstance<ActionBlock<string>>(_serviceProvider);
            actionBlock
                .WithName("Test block 1")
                .WithRetries(3, 10000);

            await actionBlock.ExecuteAsync().ConfigureAwait(false);

            _logger.LogInformation("A test log event from SomeFlow.");
            await Task.CompletedTask;
        }
    }
}
