using DidactCore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidactCore.Blocks.CustomActionBlocks.Boligportal
{
    public class GetListOfAllPropertiesActionTaskBlock
    {
        private readonly IDidactDependencyInjector _didactDependencyInjector;

        public async Task ExecuteAsync()
        {

        }

        public GetListOfAllPropertiesActionTaskBlock(IDidactDependencyInjector didactDependencyInjector)
        {
            _didactDependencyInjector = didactDependencyInjector;
        }
    }
}
