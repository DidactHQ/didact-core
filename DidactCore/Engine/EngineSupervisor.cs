using DidactCore.Constants;
using DidactCore.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DidactCore.Engine
{
    public class EngineSupervisor : IEngineSupervisor
    {
        public long EngineId { get; set; }

        public Guid EngineUniversalId { get; set; }

        public string EngineState { get; set; } = EngineStates.StartingUp;

        public CancellationToken CancellationToken { get; set; }

        public DateTime EngineStateLastUpdated { get; set; } = DateTime.Now;

        public IPluginContainers PluginContainers { get; set; }

        public IServiceCollection ApplicationServiceCollection { get; set; }

        private readonly ILogger<EngineSupervisor> _logger;
        private readonly IEngineRepository _engineRepository;

        public EngineSupervisor(ILogger<EngineSupervisor> logger, IEngineRepository engineRepository)
        {
            _logger = logger;
            _engineRepository = engineRepository;
        }

        public void SetEngineState(string engineState)
        {
            EngineState = engineState;
            EngineStateLastUpdated = DateTime.Now;
        }

        public async Task CheckForEngineShutdownEventAsync()
        {
            // TODO Finish implementing
            await _engineRepository.CheckForEngineShutdownAsync();
        }

        public string GetEngineState()
        {
            return EngineState;
        }

        public DateTime GetEngineStateLastUpdated()
        {
            return EngineStateLastUpdated;
        }
    }
}
