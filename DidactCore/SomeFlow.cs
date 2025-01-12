using DidactCore.Blocks;
using DidactCore.Constants;
using DidactCore.Flows;
using System.Threading.Tasks;

namespace DidactCore
{
    public class SomeFlow : IFlow
    {
        private readonly IFlowLogger _flowLogger;
        private readonly IFlowConfigurator _flowConfigurator;
        private readonly ActionBlock<string> _block1;
        private readonly ActionBlock<int> _block2;

        public SomeFlow(IFlowLogger flowLogger, IFlowConfigurator flowConfigurator, IBlockFactory blockFactory)
        {
            _flowLogger = flowLogger;
            _flowConfigurator = flowConfigurator;
            _block1 = blockFactory.CreateActionBlock<string>();
            _block2 = blockFactory.CreateActionBlock<int>();
        }

        public async Task<IFlowConfigurator> ConfigureAsync()
        {
            await Task.CompletedTask;
            return _flowConfigurator
                .WithTypeName(GetType().Name)
                .WithName("SomeFlow Custom Name")
                .WithDescription("A flow description.")
                .AsVersion("1.0-alpha")
                .ForQueue(QueueTypes.HyperQueue, "Default")
                .WithCronSchedule("0 * * * *");
        }

        public async Task ExecuteAsync(IFlowExecutionContext context)
        {
            await _block1.ExecuteAsync();
            await _block2 .ExecuteAsync();
            await Task.CompletedTask;
        }
    }
}
