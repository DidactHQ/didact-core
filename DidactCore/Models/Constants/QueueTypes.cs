namespace DidactCore.Models.Constants
{
    public static class QueueTypes
    {
        /// <summary>
        /// <para>
        /// A queue type designated for maximum throughput by running jobs concurrently or in parallel.
        /// </para>
        /// <para>
        /// A hyper queue will attempt best-effort ordering of Flows, but ordering is not strictly guaranteed. This is to prioritize throughput.
        /// </para>
        /// <para>
        /// A hyper queue is an excellent candidate for a clustered/distributed environment.
        /// </para>
        /// </summary>
        public const string HyperQueue = "Hyper Queue";

        /// <summary>
        /// <para>
        /// A queue type designated for strict, guaranteed ordering on a first in, first out basis.
        /// </para>
        /// <para>
        /// Because ordering is strictly enforced, a fifo queue will necessarily sacrifice throughput.
        /// </para>
        /// <para>
        /// Each Flow must be successfully completed before it is removed from the queue. This behavior is enforced in the database model and is therefore compatible with distributed environments.
        /// </para>
        /// </summary>
        public const string FifoQueue = "Fifo Queue";
    }
}
