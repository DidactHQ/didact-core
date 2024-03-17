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

        public void AddServiceCollection(IServiceCollection enhancedServiceCollection)
        {
            InjectorServiceCollection = enhancedServiceCollection;
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
