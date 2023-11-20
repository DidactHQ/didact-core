using DidactCore.Exceptions;
using DidactCore.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DidactCore.Models.Flows
{
    public class FlowConfigurator
    {
        private readonly ILogger _logger;
        private readonly IFlowRepository _flowRepository;

        public string Name { get; private set; }

        public string Description { get; private set; }

        public FlowConfigurator(ILogger logger, IFlowRepository flowRepository)
        {
            _logger = logger;
            _flowRepository = flowRepository;
        }

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

        /// <summary>
        /// Asynchronously saves the Flow configurations to persistent storage.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SaveFlowConfigurationsException"></exception>
        public async Task SaveConfigurationsAsync()
        {
            try
            {
                _logger.LogInformation("Saving the Flow configurations to persistent storage...");
                await _flowRepository.SaveConfigurationsAsync(this).ConfigureAwait(false);
                _logger.LogInformation("Flow configurations saved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Flow configurations were unable to be saved successfully. See inner exception: {ex}", ex);
                var saveFlowConfigurationsException = new SaveFlowConfigurationsException("Flow configurations were unable to be saved successfully.", ex);
                throw saveFlowConfigurationsException;
            }
        }
    }
}
