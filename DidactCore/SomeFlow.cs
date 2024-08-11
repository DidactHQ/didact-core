using DidactCore.Blocks;
using DidactCore.Constants;
using DidactCore.DependencyInjection;
using DidactCore.Flows;
using System;
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
            var actionTaskBlock = _didactDependencyInjector.CreateInstance<ActionTaskBlock>();
            actionTaskBlock
                .WithExecutor(async () =>
                {
                    Console.WriteLine(jsonInputString);
                    Console.WriteLine("ran action task block!");
                    await Task.Delay(1000);
                });

            //await actionBlock.ExecuteAsync();
            await actionTaskBlock.ExecuteDelegateAsync();
            await Task.CompletedTask;

            //var actionBlock = _didactDependencyInjector.CreateInstance<ActionBlock<string>>();
            //actionBlock
            //    .WithName("Test block 1")
            //    .WithRetries(3, 10000).Action(async (input) => {await Task.Delay(1000) return input;});return input;
            //    });

            //var actionBlock = _didactDependencyInjector.CreateInstance<ActionBlock<string>>();
            //actionBlock.WithName("Test block 1")
            //.WithRetries(3, 10000).Action((input) => { await Task.Delay(1000); });
            // Perform some action with 'input', but do not return anything


        }
    }
}
