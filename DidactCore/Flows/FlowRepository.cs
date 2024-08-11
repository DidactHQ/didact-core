using DidactCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public class FlowRepository : IFlowRepository
    {
        public Task ActivateFlowByIdAsync(long flowId)
        {
            // TODO: Implement ActivateFlowByIdAsync
            //throw new NotImplementedException();

            return new Task(() => { });
        }

        public Task DeactivateFlowByIdAsync(long flowId)
        {
            // TODO: Implement DeactivateFlowByIdAsync
            //throw new NotImplementedException();

            return new Task(() => { });
        }

        public Task<IEnumerable<Flow>> GetAllFlowsFromStorageAsync()
        {
            // TODO: Implement GetAllFlowsFromStorageAsync
            //throw new NotImplementedException();

            return new Task<IEnumerable<Flow>>(() => new List<Flow>());
        }

        public Task<IEnumerable<Flow>> GetAllOrganizationFlowsFromStorageAsync(int organizationId)
        {
            // TODO: Implement GetAllOrganizationFlowsFromStorageAsync
            //throw new NotImplementedException();

            return new Task<IEnumerable<Flow>>(() => new List<Flow>());
        }

        public Task<Flow> GetFlowByIdAsync(long flowId)
        {
            // TODO: Implement GetFlowByIdAsync
            //throw new NotImplementedException();

            return new Task<Flow>(() => new Flow());
        }

        public Task<Flow> GetFlowByNameAsync(string name)
        {
            // TODO: Implement GetFlowByNameAsync
            //throw new NotImplementedException();

            return new Task<Flow>(() => new Flow());
        }

        public Task<Flow> GetFlowByTypeNameAsync(string flowTypeName)
        {
            // TODO: Implement GetFlowByTypeNameAsync
            //throw new NotImplementedException();

            return new Task<Flow>(() => new Flow());
        }

        public Task SaveConfigurationsAsync(IFlowConfigurator flowConfigurator)
        {
            // TODO: Implement SaveConfigurationsAsync
            //throw new NotImplementedException();

            return new Task(() => { });
        }
    }
}