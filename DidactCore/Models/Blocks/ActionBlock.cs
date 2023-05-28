using DidactCore.Exceptions;
using DidactCore.Models.Constants;
using DidactCore.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
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

        public int RetryAttemptsThreshold { get; set; }

        public int RetryDelayMilliseconds { get; set; }

        public int Timeout { get; set; }

        public int RetriesAttempted { get; private set; }

        public int RuntimeMinutesElapsed { get; private set; }

        public bool TimeoutExceeded { get; private set; }


        public ActionBlock(ILogger logger, IBlockRepository blockRepository)
        {
            _logger = logger;
            _blockRepository = blockRepository;
        }

        public ActionBlock<T> WithExecutor(Action<T> action)
        {
            Action = action;
            return this;
        }

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

        public ActionBlock<T> WithTimeout(int timeoutThreshold)
        {
            Timeout = timeoutThreshold;
            return this;
        }

        public ActionBlock<T> WithRetries(int retryAttemptsThreshold, int retryDelayMilliseconds)
        {
            RetryAttemptsThreshold = retryAttemptsThreshold;
            RetryDelayMilliseconds = retryDelayMilliseconds;
            return this;
        }

        public async Task ExecuteDelegateAsync()
        {
            if (Action == default || Parameter == null)
            {
                throw new NullBlockExecutorException("The executor was not properly satisfied.");
            }

            while (RetriesAttempted <= RetryAttemptsThreshold)
            {
                try
                {
                    State = BlockState.Running;
                    _logger.LogInformation("Action Block {name} executing delegate...", Name);
                    Action(Parameter);
                    State = BlockState.Succeeded;
                }
                catch (Exception ex)
                {
                    if (RetriesAttempted <= RetryAttemptsThreshold)
                    {
                        State = BlockState.Failing;
                        _logger.LogError(ex, "Action Block {name} encountered an unhandled exception. See details: {ex}", Name, JsonSerializer.Serialize(ex, new JsonSerializerOptions(JsonSerializerDefaults.Web)));
                        _logger.LogInformation("Action Block {name} incrementing retry attempts and awaiting retry delay...", Name);
                        RetriesAttempted++;
                        await Task.Delay(RetryDelayMilliseconds);
                        State = BlockState.Retrying;
                        _logger.LogInformation("Action Block {name} attempting retry...", Name);
                        continue;
                    }
                    else
                    {
                        State = BlockState.Failed;
                        _logger.LogCritical(ex, "Action Block {name} has encountered an unhandled exception and has now failed. See details: {ex}", Name, JsonSerializer.Serialize(ex, new JsonSerializerOptions(JsonSerializerDefaults.Web)));
                        throw;
                    }
                }
            }
        }

        public async Task ExecuteAsync()
        {
            var timeoutTask = Task.Delay(Timeout);
            var delegateTask = ExecuteDelegateAsync();

            if (timeoutTask == await Task.WhenAny(delegateTask, timeoutTask))
            {
                State = BlockState.Failed;
                _logger.LogCritical("Action Block {name} exceeded its timeout threshold.", Name);
                throw new TimeoutException($"Action Block {Name} exceeded its timeout threshold.");
            }

            await delegateTask;
        }
    }
}
