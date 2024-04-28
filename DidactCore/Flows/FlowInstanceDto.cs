using DidactCore.Entities;

namespace DidactCore.Flows
{
    public class FlowInstanceDto
    {
        public Flow Flow { get; set; }

        public IFlow FlowInstance { get; set; }
    }
}
