using DidactCore.DependencyInjection;
using System.Threading.Tasks;

namespace DidactCore.Blocks.CustomActionBlocks.Boligportal
{
    public class BoligportalOrchestratorActionTaskBlock
    {
        private readonly IDidactDependencyInjector _didactDependencyInjector;

        public async Task ExecuteAsync()
        {
            // call all the other action task blocks in the correct order - plus ability to rerun specific action task blocks that failed while preserving the state of the other action task blocks and data allready fetched and saved to database

            // GetListOfAllPropertiesActionTaskBlock in batch of 18 per page
            // GetSpecificPropertyActionTaskBlock for batch of 18 properties
            // ParseBoligportalHTMLPageToJsonActionTaskBlock
            //ConvertBoligportalPropertyToCustomPropertyModelActionTaskBlock
            // UploadPropertiesToDatabaseActionTaskBlock
        }

        public BoligportalOrchestratorActionTaskBlock(IDidactDependencyInjector didactDependencyInjector)
        {
            _didactDependencyInjector = didactDependencyInjector;
        }
    }
}
