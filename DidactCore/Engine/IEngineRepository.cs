using System.Threading.Tasks;

namespace DidactCore.Engine
{
    public interface IEngineRepository
    {
        Task CheckForEngineShutdownAsync();
    }
}
