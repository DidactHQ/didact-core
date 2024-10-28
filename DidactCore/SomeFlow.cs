using DidactCore.Constants;
using DidactCore.Flows;
using System.Threading.Tasks;

namespace DidactCore
{
    public class SomeFlow : IFlow
    {
        private readonly IFlowLogger _flowLogger;
        private readonly IFlowConfigurator _flowConfigurator;

        public SomeFlow(IFlowLogger flowLogger, IFlowConfigurator flowConfigurator)
        {
            _flowLogger = flowLogger;
            _flowConfigurator = flowConfigurator;
        }

        public IFlowConfigurator Configure()
        {
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
            await Task.CompletedTask;
        }
    }
}
