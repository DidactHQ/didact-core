using Microsoft.Extensions.DependencyInjection;
using System;

namespace DidactCore.DependencyInjection
{
    public interface IDidactDependencyInjector
    {
        IServiceCollection ApplicationServiceCollection { get; set; }

        IServiceCollection FlowServiceCollection { get; set; }

        IServiceProvider FlowServiceProvider { get; set; }

        /// <summary>
        /// Clears the <see cref="FlowServiceCollection"/> and resets it to the <see cref="ApplicationServiceCollection"/>.
        /// </summary>
        void ResetServiceCollection();

        /// <summary>
        /// Adds each service from the bridgeServiceCollection to the <see cref="FlowServiceCollection"/>
        /// and rebuilds the <see cref="FlowServiceProvider"/>.
        /// </summary>
        /// <param name="bridgeServiceCollection"></param>
        void AddAndRebuildServiceCollection(IServiceCollection bridgeServiceCollection);
    }
}
