using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DidactCore.DependencyInjection
{
    public class DidactDependencyInjector
    {
        public IServiceCollection ApplicationServiceCollection { get; set; }

        public IServiceCollection FlowServiceCollection { get; set; }

        public IServiceProvider FlowServiceProvider { get; set; }

        public DidactDependencyInjector(IServiceCollection applicationServiceCollection)
        {
            ApplicationServiceCollection = applicationServiceCollection;
            FlowServiceCollection = applicationServiceCollection;
            FlowServiceProvider = FlowServiceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Clears the InjectorServiceCollection and resets it to the ApplicationServiceCollection.
        /// </summary>
        public void ResetServiceCollection()
        {
            FlowServiceCollection.Clear();
            FlowServiceCollection = ApplicationServiceCollection;
        }

        /// <summary>
        /// Adds each service from the bridgeServiceCollection to the InjectorServiceCollection,
        /// then rebuilds the InjectorServiceCollection.
        /// </summary>
        /// <param name="bridgeServiceCollection"></param>
        public void AddAndRebuildServiceCollection(IServiceCollection bridgeServiceCollection)
        {
            foreach (var service in bridgeServiceCollection)
            {
                FlowServiceCollection.TryAdd(service);
            }

            FlowServiceProvider = FlowServiceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Builds the InjectorServiceCollection into an IServiceProvider.
        /// </summary>
        public void BuildServiceCollection()
        {
            FlowServiceProvider = FlowServiceCollection.BuildServiceProvider();
        }
    }
}
