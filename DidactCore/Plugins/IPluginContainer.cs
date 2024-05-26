using DidactCore.DependencyInjection;
using System;
using System.Linq;

namespace DidactCore.Plugins
{
    public interface IPluginContainer
    {
        PluginLoadContext PluginLoadContext { get; set; }

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
