﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace DidactCore.Plugins
{
    public interface IPluginContainer
    {
        PluginAssemblyLoadContext PluginAssemblyLoadContext { get; set; }

        ICollection<PluginExecutionVersion> PluginExecutionVersions { get; }

        DateTime PluginLoadedAt { get; set; }

        IServiceCollection ApplicationServiceCollection { get; set; }

        IPluginDependencyInjector PluginDependencyInjector { get; set; }

        /// <summary>
        /// Gets an enumeration of the assemblies from the plugin container's <see cref="DidactCore.Plugins.PluginAssemblyLoadContext"/>.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies() => PluginAssemblyLoadContext.Assemblies;

        /// <summary>
        /// Sets a timestamp for when the plugin was loaded into the plugin container.
        /// </summary>
        /// <param name="pluginLoadedAt"></param>
        void SetPluginLoadedAt(DateTime pluginLoadedAt);

        /// <summary>
        /// Configures the plugin's dependency injection system.
        /// </summary>
        void ConfigureDependencyInjection();

        /// <summary>
        /// Retrieves and instantiates all Flow types to generate their configurators.
        /// Then uses the configurators and reflection to determine each Flow's <see cref="PluginExecutionVersion"/>.
        /// </summary>
        void CollectPluginExecutionVersions();

        /// <summary>
        /// Retrieves all Flow types from the plugin's assemblies using reflection,
        /// instantiates each Flow type using the plugin dependency injection system,
        /// and runs their configuration functions.
        /// If configuration fails for a specific set of Flows,
        /// gracefully handles the failures and passes through the successes.
        /// </summary>
        /// <returns></returns>
        Task ConfigureFlowsAsync();
    }
}
