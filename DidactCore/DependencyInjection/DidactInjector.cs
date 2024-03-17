using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DidactCore.DependencyInjection
{
    public class DidactInjector
    {
        public IServiceCollection ApplicationServiceCollection { get; set; }

        public IServiceCollection MasterServiceCollection { get; set; }

        public IServiceProvider MasterServiceProvider { get; set; }

        public DidactInjector(IServiceCollection applicationServiceCollection)
        {
            ApplicationServiceCollection = applicationServiceCollection;
            MasterServiceCollection = applicationServiceCollection;
            MasterServiceProvider = MasterServiceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Clears the InjectorServiceCollection and resets it to the ApplicationServiceCollection.
        /// </summary>
        public void ResetServiceCollection()
        {
            MasterServiceCollection.Clear();
            MasterServiceCollection = ApplicationServiceCollection;
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
                MasterServiceCollection.TryAdd(service);
            }

            MasterServiceProvider = MasterServiceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Builds the InjectorServiceCollection into an IServiceProvider.
        /// </summary>
        public void BuildServiceCollection()
        {
            MasterServiceProvider = MasterServiceCollection.BuildServiceProvider();
        }
    }
}
