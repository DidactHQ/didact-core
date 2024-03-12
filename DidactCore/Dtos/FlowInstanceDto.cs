using DidactCore.Entities;
using DidactCore.Flows;

namespace DidactCore.Dtos
{
    public class FlowInstanceDto
    {
        public Flow Flow { get; set; }

        public IFlow FlowInstance { get; set; }
    }
}
