using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DidactCore.Models.Blocks
{
    public class ActionBlockAsync
    {
        private readonly ILogger<ActionBlockAsync> _logger;

        public Func<Task> Executor { get; private set; }
        
        public ActionBlockAsync(ILogger<ActionBlockAsync> logger)
        {
            _logger = logger;
        }

        public async Task ExecuteDelegateAsyn()
        {
            _logger.LogInformation("Executing delegate...");
            await Executor.Invoke().ConfigureAwait(false);
        }
    }
}
