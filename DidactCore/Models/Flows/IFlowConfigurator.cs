using DidactCore.Models.Constants;
using System;
using System.Threading.Tasks;

namespace DidactCore.Models.Flows
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
        /// The designated queue type that the Flow will execute against. The default type is HyperQueue.
        /// </summary>
        /// <see cref="QueueTypes.HyperQueue"/>
        string QueueType { get; }

        /// <summary>
        /// The designated queue that the Flow will execute against. The default name is "default".
        /// </summary>
        string QueueName { get; }

        /// <summary>
        /// The CRON schedule for the Flow.
        /// </summary>
        string CronExpression { get; }

        /// <summary>
        /// The optional start datetime of the Flow's CRON schedule.
        /// </summary>
        DateTime? StartDateTime { get; }

        /// <summary>
        /// The optional end datetime of the Flow's CRON schedule.
        /// </summary>
        DateTime? EndDateTime { get; }

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
        /// Sets the Flow to execute for a specific queue type and queue.
        /// </summary>
        /// <param name="queueType"></param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        IFlowConfigurator ForQueue(string queueType, string queueName);

        /// <summary>
        /// <para>
        ///     Sets a CRON schedule for the Flow.
        /// </para>
        /// <para>
        ///     The schedule has an optional start datetime and end datetime.
        /// </para>
        /// </summary>
        /// <param name="cronExpression"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        IFlowConfigurator WithCronSchedule(string cronExpression, DateTime? startDateTime = null, DateTime? endDateTime = null);

        /// <summary>
        /// Asynchronously saves the Flow configurations to persistent storage.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SaveFlowConfigurationsException"></exception>
        Task SaveConfigurationsAsync();
    }
}
