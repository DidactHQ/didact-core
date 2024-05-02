using DidactCore.Entities;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public interface IFlowRunRepository
    {
        Task<FlowRun> GetFlowRunAsync(long flowRunId);

        Task<FlowRun> CreateAndEnqueueFlowRunAsync(FlowRun flowRun);

        Task<FlowRun> CreateAndExecuteFlowRunAsync(FlowRun flowRun);

        Task<FlowRun> UpdateFlowRunAsync(FlowRun flowRun);

        Task DeleteFlowRunAsync(long flowRunId);
    }
}
