using DidactCore.Models.Flows;
using System.Threading.Tasks;

namespace DidactCore.Repositories
{
    public interface IFlowRepository
    {
        Task SaveConfigurationsAsync(FlowConfigurator flowConfigurator);
    }
}
