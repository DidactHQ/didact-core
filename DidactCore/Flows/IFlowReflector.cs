namespace DidactCore.Flows
{
    public interface IFlowReflector
    {
        /// <summary>
        /// Retrieves the Flow type from the AppDomain's assemblies using reflection
        /// and instantiates the Flow using the dependency injection system.
        /// </summary>
        /// <param name="flowTypeName"></param>
        /// <returns></returns>
        IFlow CreateFlow(string flowTypeName);
    }
}
