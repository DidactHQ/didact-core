using DidactCore.Entities;

namespace DidactCore.Flows
{
    public class FlowRunDto
    {
        public FlowRun FlowRun { get; set; } = null!;

        public FlowVersion FlowVersion { get; set; } = null!;

        public Flow Flow { get; set; } = null!;

        public IFlow? FlowInstance { get; set; }
    }
}
