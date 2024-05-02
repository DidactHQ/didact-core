using DidactCore.Entities;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public interface IFlowRunRepository
    {
        Task<FlowRun> GetFlowRunByIdAsync(long flowRunId);

        Task<FlowRun> CreateAndEnqueueFlowRunAsync(FlowRun flowRun);

        Task<FlowRun> CreateAndExecuteFlowRunAsync(FlowRun flowRun);
    }
}
