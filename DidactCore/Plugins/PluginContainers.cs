using System;
using System.Collections.Generic;

namespace DidactCore.Plugins
{
    public interface PluginContainers
    {
        ICollection<IPluginContainer> PluginContainers { get; set; }
        DateTime PluginContainersLastUpdatedAt { get; set; }

        void AddPluginContainer(IPluginContainer pluginContainer);
        IPluginContainer FindMatchingPluginContainer(string assemblyName, string assemblyVersion);
        IPluginContainer FindMatchingPluginContainer(string assemblyName, string assemblyVersion, string typeName);
        IPluginContainer FindMatchingPluginContainer(Type type);
        void RemovePluginContainer(IPluginContainer pluginContainer);
        IPluginContainers SetPluginContainersLastUpdatedAt(DateTime? pluginContainersLastUpdatedAt);
    }
}