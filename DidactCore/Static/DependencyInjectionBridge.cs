using Microsoft.Extensions.DependencyInjection;
using System;

namespace DidactCore.Static
{
    public static class DependencyInjectionBridge
    {
        /// <summary>
        /// A clone of the main service collection from the Didact Engine.
        /// </summary>
        public static IServiceCollection BaseServiceCollection { get; set; } = new ServiceCollection();

        /// <summary>
        /// The dynamic service collection that contains not only the services from the main service collection but also
        /// the additionally-registered services from the external class library.
        /// </summary>
        public static IServiceCollection DynamicServiceCollection { get; set; } = new ServiceCollection();

        /// <summary>
        /// A clone of the service provider for the <see cref="BaseServiceCollection"/>. This should only be called once, automatically, during app startup in Program.cs.
        /// </summary>
        public static IServiceProvider BaseServiceProvider { get; set; } = BaseServiceCollection.BuildServiceProvider();

        /// <summary>
        /// The service provider for the <see cref="DynamicServiceCollection"/>. This should be called each time a new set of DLLs are read and loaded via the polling engine.
        /// </summary>
        public static IServiceProvider DynamicServiceProvider { get; set; } = DynamicServiceCollection.BuildServiceProvider();

        /// <summary>
        /// Sets the service collection for <see cref="BaseServiceCollection"/>.
        /// </summary>
        /// <param name="externalServiceCollection"></param>
        public static void SetBaseServiceCollection(IServiceCollection externalServiceCollection)
        {
            BaseServiceCollection = externalServiceCollection;
        }

        /// <summary>
        /// Sets the service collection for <see cref="DynamicServiceCollection"/>.
        /// </summary>
        /// <param name="dynamicServiceCollection"></param>
        public static void SetDynamicServiceCollection(IServiceCollection dynamicServiceCollection)
        {
            DynamicServiceCollection = dynamicServiceCollection;
        }

        /// <summary>
        /// Builds the <see cref="BaseServiceCollection"/> into the <see cref="BaseServiceProvider"/>.
        /// </summary>
        public static void BuildBaseServiceCollection()
        {
            BaseServiceProvider = BaseServiceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Builds the <see cref="DynamicServiceCollection"/> into the <see cref="DynamicServiceProvider"/>.
        /// </summary>
        public static void BuildDynamicServiceCollection()
        {
            DynamicServiceProvider = DynamicServiceCollection.BuildServiceProvider();
        }
    }
}