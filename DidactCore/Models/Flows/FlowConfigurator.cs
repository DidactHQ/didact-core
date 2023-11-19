namespace DidactCore.Models.Flows
{
    public class FlowConfigurator
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        /// <summary>
        /// Sets the Flow name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FlowConfigurator WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        /// Sets the Flow description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public FlowConfigurator WithDescription(string description)
        {
            Description = description;
            return this;
        }
    }
}
