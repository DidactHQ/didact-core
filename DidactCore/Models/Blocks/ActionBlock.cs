using DidactCore.Exceptions;
using DidactCore.Models.Constants;
using DidactCore.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DidactCore.Models.Blocks
{
    public class ActionBlock<T>
    {
        private readonly ILogger _logger;
        private readonly IBlockRepository _blockRepository;

        public Action<T> Action { get; set; }

        public T Parameter { get; set; }

        public string Name { get; set; }

        public string State { get; set; } = BlockState.Idle;

        public int RetryAttemptsThreshold { get; set; } = 0;

        public int RetryDelayMilliseconds { get; set; }

        public int SoftTimeout { get; set; }

        public int RetriesAttempted { get; private set; } = 0;

        public int RuntimeMinutesElapsed { get; private set; }

        public bool SoftTimeoutExceeded { get; private set; }


        public ActionBlock(ILogger logger, IBlockRepository blockRepository)
        {
            _logger = logger;
            _blockRepository = blockRepository;
        }

        /// <summary>
        /// Sets the delegate to execute. Since this is a delegate, it can be many things, like an expression tree that you define in this constructor or a method from somewhere else.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ActionBlock<T> WithExecutor(Action<T> action)
        {
            Action = action;
            return this;
        }

        /// <summary>
        /// Sets the arguments for the delegate.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionBlock<T> WithArgs(T param)
        {
            Parameter = param;
            return this;
        }

        public ActionBlock<T> WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        /// Sets the soft timeout threshold for the block.
        /// The soft timeout will NOT abort the delegate's execution, but it will check for a violation before and after the delegate's execution and retries.
        /// </summary>
        /// <param name="softTimeoutThreshold"></param>
        /// <returns></returns>
        public ActionBlock<T> WithSoftTimeout(int softTimeoutThreshold)
        {
            SoftTimeout = softTimeoutThreshold;
            return this;
        }

        public ActionBlock<T> WithRetries(int retryAttemptsThreshold, int retryDelayMilliseconds)
        {
            RetryAttemptsThreshold = retryAttemptsThreshold;
            RetryDelayMilliseconds = retryDelayMilliseconds;
            return this;
        }

        private async Task ExecuteDelegateAsync()
        {
            if (Action == null || Parameter == null)
            {
                throw new NullBlockExecutorException("The executor or its arguments were not properly satisfied.");
            }

            while (RetriesAttempted <= RetryAttemptsThreshold)
            {
                if (SoftTimeoutExceeded)
                {
                    _logger.LogInformation("The soft timeout threshold has been exceeded. Cancelling execution...");
                    break;
                }

                try
                {
                    State = BlockState.Running;
                    _logger.LogInformation("Action Block {name} executing delegate...", Name);
                    Action(Parameter);
                    State = BlockState.Succeeded;
                    break;
                }
                catch (Exception ex)
                {
                    RetriesAttempted++;

                    if (RetriesAttempted <= RetryAttemptsThreshold)
                    {
                        if (SoftTimeoutExceeded)
                        {
                            _logger.LogInformation("The soft timeout threshold has been exceeded. Cancelling execution...");
                            break;
                        }

                        State = BlockState.Failing;
                        _logger.LogError("Action Block {name} encountered an unhandled exception. See details: {ex}", Name, ex.Message);
                        _logger.LogInformation("Action Block {name} awaiting retry delay...", Name);
                        await Task.Delay(RetryDelayMilliseconds);
                        State = BlockState.Retrying;
                        _logger.LogInformation("Action Block {name} attempting retry...", Name);

                        if (SoftTimeoutExceeded)
                        {
                            _logger.LogInformation("The soft timeout threshold has been exceeded. Cancelling execution...");
                            break;
                        }

                        continue;
                    }
                    else
                    {
                        State = BlockState.Failed;
                        _logger.LogCritical("Action Block {name} has encountered an unhandled exception and has now failed. See details: {ex}", Name, ex.Message);
                        throw;
                    }
                }
            }
        }

        public async Task ExecuteAsync()
        {
            var timeoutTask = Task.Delay(SoftTimeout);
            var delegateTask = ExecuteDelegateAsync();

            if (timeoutTask == await Task.WhenAny(delegateTask, timeoutTask))
            {
                State = BlockState.Failed;
                _logger.LogCritical("Action Block {name} exceeded its soft timeout threshold. Marking for cancellation...", Name);
                SoftTimeoutExceeded = true;
            }

            await delegateTask;
        }
    }
}
