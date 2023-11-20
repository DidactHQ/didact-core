using System.Threading.Tasks;

namespace DidactCore.Models.Flows
{
    public interface IFlowConfigurator
    {
        string Name { get; }

        string Description { get; }

        IFlowConfigurator WithName(string name);

        IFlowConfigurator WithDescription(string description);

        Task SaveConfigurationsAsync();
    }
}
