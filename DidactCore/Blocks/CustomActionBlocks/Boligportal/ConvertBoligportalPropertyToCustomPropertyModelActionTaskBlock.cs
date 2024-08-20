using DidactCore.DependencyInjection;
using System.Threading.Tasks;

namespace DidactCore.Blocks.GenericActionBlocks
{
    public class ConvertBoligportalPropertyToCustomPropertyModelActionTaskBlock
    {
        private readonly IDidactDependencyInjector _didactDependencyInjector;

        public async Task ExecuteAsync()
        {
            
        }

        public ConvertBoligportalPropertyToCustomPropertyModelActionTaskBlock(IDidactDependencyInjector didactDependencyInjector)
        {
            _didactDependencyInjector = didactDependencyInjector;
        }
    }
}
