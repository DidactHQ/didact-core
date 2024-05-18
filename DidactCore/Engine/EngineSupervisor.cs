using Microsoft.Extensions.Logging;
using System;

namespace DidactCore.Engine
{
    public class EngineSupervisor : IEngineSupervisor
    {
        public string EngineState { get; set; } = "Unspecified";

        public DateTime EngineStateLastUpdated { get; set; } = DateTime.Now;

        private readonly ILogger<EngineSupervisor> _logger;

        public EngineSupervisor(ILogger<EngineSupervisor> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void SetEngineState(string engineState)
        {
            EngineState = engineState;
            EngineStateLastUpdated = DateTime.Now;
        }
    }
}
