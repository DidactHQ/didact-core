using Microsoft.Extensions.Logging;
using System;

namespace DidactCore.Blocks
{
    public class DelegateBlock
    {
        private readonly ILogger<DelegateBlock> _logger;
        private readonly IBlockRepository _blockRepository;

        public Delegate Delegate { get; private set; } = null!;

        public object[] Arguments { get; private set; } = null!;

        public DelegateBlock(ILogger<DelegateBlock> logger, IBlockRepository blockRepository)
        {
            _logger = logger;
            _blockRepository = blockRepository;
        }

        public DelegateBlock WithDelegate(Delegate aDelegate)
        {
            Delegate = aDelegate;
            return this;
        }

        public DelegateBlock WithArguments(object[] arguments)
        {
            Arguments = arguments;
            return this;
        }
    }
}
