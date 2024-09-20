using DidactCore.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DidactCore.Blocks.GenericActionBlocks
{
    public class SqlActionTaskBlock
    {
        private readonly IDidactDependencyInjector _didactDependencyInjector;

        public async Task ExecuteAsync()
        {
            // SQL CRUD operations
        }

        public SqlActionTaskBlock(IDidactDependencyInjector didactDependencyInjector)
        {
            _didactDependencyInjector = didactDependencyInjector;
        }
    }
}
