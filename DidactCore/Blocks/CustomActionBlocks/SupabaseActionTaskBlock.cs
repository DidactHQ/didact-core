using DidactCore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DidactCore.Blocks.CustomActionBlocks
{
    public class SupabaseActionTaskBlock
    {
        private readonly IDidactDependencyInjector _didactDependencyInjector;

        public async Task ExecuteAsync()
        {
            // call supabase api to insert a new row

            // call supabase api to read all rows
        }

        public SupabaseActionTaskBlock(IDidactDependencyInjector didactDependencyInjector)
        {
            _didactDependencyInjector = didactDependencyInjector;
        }
    }
}
