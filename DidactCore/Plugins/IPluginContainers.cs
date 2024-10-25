using System;
using System.Collections.Generic;
using System.Linq;

namespace DidactCore.Plugins
{
    public interface IPluginContainers
    {
        ICollection<IPluginContainer> PluginContainers { get; set; }

        DateTime PluginContainersLastUpdatedAt { get; set; }

        IPluginContainers SetPluginContainersLastUpdatedAt(DateTime? pluginContainersLastUpdatedAt)
        {
            PluginContainersLastUpdatedAt = pluginContainersLastUpdatedAt ?? DateTime.UtcNow;
            return this;
        }

        /// <summary>
        /// Finds a matching <see cref="IPluginContainer"/> for the given <see cref="Type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NoMatchedPluginException"></exception>
        /// <exception cref="MultipleMatchedPluginsException"></exception>
        IPluginContainer FindMatchingPluginContainer(Type type)
        {
            var assemblyFullName = type.Assembly.FullName;
            var matchingPluginContainers = PluginContainers.Select(s => s)
                .Where(p => p.PluginAssemblyLoadContext.Assemblies.Select(a => a.FullName).Contains(assemblyFullName)
                    && p.PluginAssemblyLoadContext.Assemblies.SelectMany(s => s.GetTypes()).Contains(type))
                .ToList();

            if (matchingPluginContainers.Count == 0)
            {
                throw new NoMatchedPluginException();
            }
            if (matchingPluginContainers.Count > 1)
            {
                throw new MultipleMatchedPluginsException();
            }

            return matchingPluginContainers.First();
        }

        /// <summary>
        /// Finds a matching <see cref="IPluginContainer"/> for the given <see cref="PluginAssemblyLoadContext"/>.
        /// </summary>
        /// <param name="pluginExecutionVersion"></param>
        /// <returns></returns>
        /// <exception cref="NoMatchedPluginException"></exception>
        IPluginContainer FindMatchingPluginContainer(PluginExecutionVersion pluginExecutionVersion)
        {
            var matchingPluginContainers = PluginContainers.Select(s => s)
                .Where(p => p.PluginExecutionVersions.Contains(pluginExecutionVersion))
                .ToList();

            if (matchingPluginContainers.Count == 0)
            {
                throw new NoMatchedPluginException();
            }

            if (matchingPluginContainers.Count > 1)
            {
                return matchingPluginContainers.OrderByDescending(p => p.PluginLoadedAt).First();
            }

            return matchingPluginContainers.First();
        }

        /// <summary>
        /// Finds a matching <see cref="IPluginContainer"/> for the given assembly FullName.
        /// </summary>
        /// <param name="assemblyFullName"></param>
        /// <returns></returns>
        /// <exception cref="NoMatchedPluginException"></exception>
        IPluginContainer FindMatchingPluginContainer(string assemblyName, string assemblyVersion)
        {
            var matchingPluginContainers = PluginContainers.Select(s => s)
                .Where(p => p.PluginAssemblyLoadContext.Assemblies.Select(a => a.GetName().Name).Contains(assemblyName)
                    && p.PluginAssemblyLoadContext.Assemblies.Select(a => a.GetName().Version?.ToString() ?? string.Empty).Contains(assemblyVersion))
                .ToList();

            if (matchingPluginContainers.Count == 0)
            {
                throw new NoMatchedPluginException();
            }
            if (matchingPluginContainers.Count > 1)
            {
                return matchingPluginContainers.OrderByDescending(p => p.PluginLoadedAt).First();
            }

            return matchingPluginContainers.First();
        }

        /// <summary>
        /// Finds a matching <see cref="IPluginContainer"/> for the given assembly FullName and type Name.
        /// </summary>
        /// <param name="assemblyFullName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        /// <exception cref="NoMatchedPluginException"></exception>
        IPluginContainer FindMatchingPluginContainer(string assemblyName, string assemblyVersion, string typeName)
        {
            var matchingPluginContainers = PluginContainers.Select(s => s)
                .Where(p => p.PluginAssemblyLoadContext.Assemblies.Select(a => a.FullName).Contains(assemblyName)
                    && p.PluginAssemblyLoadContext.Assemblies.Select(a => a.GetName().Version?.ToString() ?? string.Empty).Contains(assemblyVersion)
                    && p.PluginAssemblyLoadContext.Assemblies.SelectMany(a => a.GetTypes()).Select(t => t.Name).Contains(typeName))
                .ToList();

            if (matchingPluginContainers.Count == 0)
            {
                throw new NoMatchedPluginException();
            }

            if (matchingPluginContainers.Count > 1)
            {
                return matchingPluginContainers.OrderByDescending(p => p.PluginLoadedAt).First();
            }

            return matchingPluginContainers.First();
        }

        void AddPluginContainer(IPluginContainer pluginContainer)
        {
            var matchedPluginContainers = PluginContainers.Select(p => p).Where(p => p.Equals(pluginContainer)).ToList();
            if (matchedPluginContainers.Count == 0)
            {
                PluginContainers.Add(pluginContainer);
            }
            SetPluginContainersLastUpdatedAt(DateTime.UtcNow);
        }

        void RemovePluginContainer(IPluginContainer pluginContainer)
        {
            var matchedPluginContainers = PluginContainers.Select(p => p).Where(p => p.Equals(pluginContainer)).ToList();
            if (matchedPluginContainers.Count > 0)
            {
                PluginContainers.Remove(pluginContainer);
            }
            SetPluginContainersLastUpdatedAt(DateTime.UtcNow);
        }
    }
}
