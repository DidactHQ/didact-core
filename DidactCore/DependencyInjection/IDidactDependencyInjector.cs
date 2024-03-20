using Microsoft.Extensions.DependencyInjection;
using System;

namespace DidactCore.DependencyInjection
{
    public interface IDidactDependencyInjector
    {
        IServiceCollection ApplicationServiceCollection { get; set; }

        IServiceCollection FlowServiceCollection { get; set; }

        IServiceProvider FlowServiceProvider { get; set; }

        void ResetServiceCollection();

        void AddAndRebuildServiceCollection(IServiceCollection bridgeServiceCollection);
    }
}
