namespace DidactCore.Execution
{
    public class PluginExecutionVersion
    {
        public string FlowTypeName { get; set; } = null!;

        public string FlowVersion { get; set; }

        public string LibraryAssemblyName { get; set; } = null!;

        public string LibraryAssemblyVersion { get; set; }
    }
}
