using System;
using System.Collections.Generic;
using System.Linq;

namespace DidactCore.Plugins
{
    public interface IPluginContainer
    {
        PluginLoadContext PluginLoadContext { get; set; }

        ICollection<PluginExecutionVersion> PluginExecutionVersions { get; }

        DateTime PluginLoadedAt { get; set; }

        IPluginDependencyInjector PluginDependencyInjector { get; set; }

        int GetAssemblyCount()
        {
            return PluginLoadContext.Assemblies.Count();
        }

        IPluginContainer SetPluginLoadedAt(DateTime pluginLoadedAt)
        {
            PluginLoadedAt = pluginLoadedAt;
            return this;
        }
    }
}
