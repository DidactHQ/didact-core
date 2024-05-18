using System;

namespace DidactCore.Engine
{
    public interface IEngineSupervisor
    {
        string EngineState { get; set; }

        DateTime EngineStateLastUpdated { get; set; }

        void SetEngineState(string engineState);

        string GetEngineState();

        DateTime GetEngineStateLastUpdated();
    }
}
