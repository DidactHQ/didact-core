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

        /// <summary>
        /// Gets an enumeration of the assemblies from the plugin container's <see cref="DidactCore.Plugins.PluginAssemblyLoadContext"/>.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies() => PluginAssemblyLoadContext.Assemblies;

        IPluginContainer SetPluginLoadedAt(DateTime pluginLoadedAt)
        {
            PluginLoadedAt = pluginLoadedAt;
            return this;
        }
    }
}
