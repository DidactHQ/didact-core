using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DidactCore.Blocks
{
    /// <summary>
    /// An asynchronous execution wrapper that takes an input of type T and returns no output.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionTaskBlock
    {
        private readonly ILogger<ActionTaskBlock> _logger;

        public Func<Task> Executor { get; private set; }

        public ActionTaskBlock(ILogger<ActionTaskBlock> logger)
        {
            _logger = logger;
        }

        public async Task ExecuteDelegateAsync()
        {
            _logger.LogInformation("Executing delegate...");
            await Executor.Invoke().ConfigureAwait(false);
        }

        public ActionTaskBlock WithExecutor(Func<Task> executor)
        {
            Executor = executor;
            return this;
        }
    }
}
