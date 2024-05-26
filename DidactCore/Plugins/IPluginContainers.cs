using System;
using System.Collections.Generic;
using System.Linq;

namespace DidactCore.Plugins
{
    public interface IPluginContainers
    {
        ICollection<IPluginContainer> PluginContainers { get; set; }

        DateTime PluginContainersLastUpdatedAt { get; set; }

        IPluginContainers SetPluginContainersLastUpdatedAt(DateTime pluginContainersLastUpdatedAt)
        {
            PluginContainersLastUpdatedAt = pluginContainersLastUpdatedAt;
            return this;
        }

        void FindMatchingPluginContainer(Type type)
        {
            var assemblyFullName = type.Assembly.FullName;
            var matchingPluginContainers = PluginContainers.Select(s => s)
                .Where(p => p.PluginLoadContext.Assemblies.Select(a => a.FullName).Contains(assemblyFullName))
                .ToList();

            if (matchingPluginContainers.Count == 0)
            {

            }
            if (matchingPluginContainers.Count > 1)
            {

            }
        }
    }
}
