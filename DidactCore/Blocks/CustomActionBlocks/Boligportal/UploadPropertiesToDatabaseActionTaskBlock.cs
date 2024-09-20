using DidactCore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DidactCore.Blocks.GenericActionBlocks
{
    public class UploadPropertiesToDatabaseActionTaskBlock
    {
        private readonly IDidactDependencyInjector _didactDependencyInjector;

        public async Task ExecuteAsync()
        {
        }

        public UploadPropertiesToDatabaseActionTaskBlock(IDidactDependencyInjector didactDependencyInjector)
        {
            _didactDependencyInjector = didactDependencyInjector;
        }
    }
}
