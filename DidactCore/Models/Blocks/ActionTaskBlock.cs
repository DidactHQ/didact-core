using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DidactCore.Models.Blocks
{
    public class ActionTaskBlock
    {
        private readonly ILogger<ActionTaskBlock> _logger;

        public Func<Task> Executor { get; private set; }
        
        public ActionTaskBlock(ILogger<ActionTaskBlock> logger)
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
