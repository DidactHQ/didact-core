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

        public int RetryAttemptsThreshold { get; set; }

        public int RetryDelayMilliseconds { get; set; }

        public decimal Timeout { get; set; }

        public int RetriesAttempted { get; private set; }

        public int RuntimeMinutesElapsed { get; private set; }


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

        public ActionBlock<T> WithTimeout(decimal timeoutThreshold)
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

        public async Task ExecuteAsync()
        {
            while (RetriesAttempted <= RetryAttemptsThreshold)
            {
                try
                {
                    Action(Parameter);
                }
                catch (Exception ex)
                {
                    if (RetriesAttempted <= RetryAttemptsThreshold)
                    {
                        _logger.LogError(ex, "Action Block {name} encountered an unhandled exception. See details: {ex}", Name, JsonSerializer.Serialize(ex, new JsonSerializerOptions(JsonSerializerDefaults.Web)));
                        _logger.LogInformation("Action Block {name} awaiting retry delay...", Name);
                        RetriesAttempted++;
                        await Task.Delay(RetryDelayMilliseconds);
                        _logger.LogInformation("Action Block {name} attempting retry...", Name);
                        continue;
                    }
                    else
                    {
                        _logger.LogCritical(ex, "Action Block {name} has encountered an unhandled exception and has now failed. See details: {ex}", Name, JsonSerializer.Serialize(ex, new JsonSerializerOptions(JsonSerializerDefaults.Web)));
                        throw;
                    }
                }
            }
        }
    }
}
