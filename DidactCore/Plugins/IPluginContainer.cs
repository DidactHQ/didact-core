using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DidactCore.Plugins
{
    public interface IPluginContainer
    {
        PluginAssemblyLoadContext PluginAssemblyLoadContext { get; set; }

        ICollection<PluginExecutionVersion> PluginExecutionVersions { get; }

        DateTime PluginLoadedAt { get; set; }

        IPluginDependencyInjector PluginDependencyInjector { get; set; }

        int GetAssemblyCount() => PluginAssemblyLoadContext.Assemblies.Count();

        IEnumerable<Assembly> GetAssemblies() => PluginAssemblyLoadContext.Assemblies;

        IPluginContainer SetPluginLoadedAt(DateTime pluginLoadedAt)
        {
            PluginLoadedAt = pluginLoadedAt;
            return this;
        }
    }
}
