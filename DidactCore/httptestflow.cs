using DidactCore.Blocks;
using DidactCore.Blocks.CustomActionBlocks;
using DidactCore.Blocks.GenericActionBlocks;
using DidactCore.Constants;
using DidactCore.DependencyInjection;
using DidactCore.Flows;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DidactCore
{
    public class HttpTestFlow : IFlow
    {
        private readonly IFlowLogger _flowLogger;
        private readonly IFlowConfigurator _flowConfigurator;
        private readonly IDidactDependencyInjector _didactDependencyInjector;

        public HttpTestFlow(IFlowLogger flowLogger, IFlowConfigurator flowConfigurator, IDidactDependencyInjector didactDependencyInjector)
        {
            _flowLogger = flowLogger;
            _flowConfigurator = flowConfigurator;
            _didactDependencyInjector = didactDependencyInjector;
        }

        public async Task ConfigureAsync()
        {
            await _flowConfigurator
                .WithName("HttpTestFlow Custom Name")
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

            var httpBlock = _didactDependencyInjector.CreateInstance<ActionTaskBlock>();  
            httpBlock.WithExecutor(async () =>
             { 
                 HttpActionTaskBlock httpActionTaskBlock = new HttpActionTaskBlock(_didactDependencyInjector);
                 
                 // Call the ExecuteAsync method with required parameters
                 await httpActionTaskBlock.ExecuteAsync(
                     url: "https://jsonplaceholder.typicode.com/todos/1", 
                     method: HttpMethod.Get);
                 
                 // Example of using database integration
                 await httpActionTaskBlock.ExecuteWithDatabaseAsync(
                     url: "https://jsonplaceholder.typicode.com/users/1",
                     method: HttpMethod.Get,
                     dbOperationType: "read");
             });            

            //await actionBlock.ExecuteAsync();
            await actionTaskBlock.ExecuteDelegateAsync();
            await httpBlock.ExecuteDelegateAsync();
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
