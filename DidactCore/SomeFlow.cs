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
