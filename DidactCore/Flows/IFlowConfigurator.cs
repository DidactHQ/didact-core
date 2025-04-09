using DidactCore.Constants;
using DidactCore.Triggers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    /// <summary>
    /// <para>A helper interface that configures Flow metadata.</para>
    /// <para>This interface and its implementations need to be registered as a transient dependency in Didact Engine.</para>
    /// </summary>
    public interface IFlowConfigurator
    {
        /// <summary>
        /// The Flow's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The Flow's description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The Flow's version.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// The Flow's Type name.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// The designated queue type that the Flow will execute against. The default type is HyperQueue.
        /// </summary>
        /// <see cref="QueueTypes.HyperQueue"/>
        string QueueType { get; }

        /// <summary>
        /// The designated queue that the Flow will execute against. The default name is "default".
        /// </summary>
        string QueueName { get; }

        /// <summary>
        /// The collection of Cron Scheduler triggers for the Flow.
        /// </summary>
        IEnumerable<ICronScheduleTrigger> CronScheduleTriggers { get; }

        /// <summary>
        /// An optional delay when enqueuing the Flow. The delay is a TimeSpan object, so it should be easy to delay by seconds, minutes, hours, etc.
        /// </summary>
        TimeSpan? Delay { get; }

        /// <summary>
        /// Sets the Flow name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IFlowConfigurator WithName(string name);

        /// <summary>
        /// Sets the Flow description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        IFlowConfigurator WithDescription(string description);

        /// <summary>
        /// Sets the Flow version.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        IFlowConfigurator AsVersion(string version);

        /// <summary>
        /// Sets the Flow Type name.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        IFlowConfigurator WithTypeName(string typeName);

        /// <summary>
        /// Sets the Flow to execute for a specific queue type and queue.
        /// </summary>
        /// <param name="queueType"></param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        IFlowConfigurator ForQueue(string queueType, string queueName = DidactDefaults.DefaultQueueName);

        /// <summary>
        /// Sets the collection of Cron Schedule triggers for the Flow.
        /// </summary>
        /// <param name="cronScheduleTriggers"></param>
        /// <returns></returns>
        IFlowConfigurator WithCronScheduleTriggers(IEnumerable<ICronScheduleTrigger> cronScheduleTriggers);

        /// <summary>
        /// Sets a delay for the Flow when it is enqueued. The delay is a TimeSpan object, so it should be easy to delay by seconds, minutes, hours, etc.
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        IFlowConfigurator WithDelay(TimeSpan delay);

        /// <summary>
        /// Asynchronously saves the Flow configurations to persistent storage.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SaveFlowConfigurationsException"></exception>
        Task SaveConfigurationsAsync();
    }
}
