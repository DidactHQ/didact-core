using Microsoft.Extensions.DependencyInjection;
using System;

namespace DidactCore.DependencyInjection
{
    public class DidactInjector
    {
        public IServiceCollection ApplicationServiceCollection { get; set; }

        public IServiceCollection InjectorServiceCollection { get; set; }

        public IServiceProvider InjectorServiceProvider { get; set; }

        public DidactInjector(IServiceCollection applicationServiceCollection)
        {
            ApplicationServiceCollection = applicationServiceCollection;
            InjectorServiceCollection = applicationServiceCollection;
            InjectorServiceProvider = InjectorServiceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Clears the InjectorServiceCollection and resets it to the ApplicationServiceCollection.
        /// </summary>
        public void ResetServiceCollection()
        {
            InjectorServiceCollection.Clear();
            InjectorServiceCollection = ApplicationServiceCollection;
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
                InjectorServiceCollection.Add(service);
            }

            InjectorServiceProvider = InjectorServiceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Builds the InjectorServiceCollection into an IServiceProvider.
        /// </summary>
        public void BuildServiceCollection()
        {
            InjectorServiceProvider = InjectorServiceCollection.BuildServiceProvider();
        }
    }
}
