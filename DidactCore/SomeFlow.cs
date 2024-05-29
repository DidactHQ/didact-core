using DidactCore.Blocks;
using DidactCore.Constants;
using DidactCore.DependencyInjection;
using DidactCore.Flows;
using System.Threading.Tasks;

namespace DidactCore
{
    public class SomeFlow : IFlow
    {
        private readonly IFlowLogger _flowLogger;
        private readonly IFlowConfigurator _flowConfigurator;
        private readonly IDidactDependencyInjector _didactDependencyInjector;

        public SomeFlow(IFlowLogger flowLogger, IFlowConfigurator flowConfigurator, IDidactDependencyInjector didactDependencyInjector)
        {
            _flowLogger = flowLogger;
            _flowConfigurator = flowConfigurator;
            _didactDependencyInjector = didactDependencyInjector;
        }

        public async Task ConfigureAsync()
        {
            await _flowConfigurator
                .WithName("SomeFlow Custom Name")
                .WithDescription("A flow description.")
                .AsVersion("1.0-alpha")
                .WithTypeName(GetType().Name)
                .ForQueue(QueueTypes.HyperQueue, "Default")
                .WithCronSchedule("0 * * * *")
                .SaveConfigurationsAsync();
        }

        public async Task ExecuteAsync(string? jsonInputString)
        {
            var actionBlock = _didactDependencyInjector.CreateInstance<ActionBlock<string>>();
            actionBlock
                .WithName("Test block 1")
                .WithRetries(3, 10000);

            var actionTaskBlock = _didactDependencyInjector.CreateInstance<ActionTaskBlock>();
            actionTaskBlock
                .WithExecutor(async () =>
                {
                    await Task.Delay(1000);
                });

            await actionBlock.ExecuteAsync();            
            await Task.CompletedTask;
        }
    }
}
