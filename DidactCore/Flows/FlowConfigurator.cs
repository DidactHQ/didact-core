﻿using DidactCore.Constants;
using DidactCore.Exceptions;
using DidactCore.Triggers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public class FlowConfigurator : IFlowConfigurator
    {
        private readonly ILogger<FlowConfigurator> _logger;
        private readonly IFlowRepository _flowRepository;

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Version { get; private set; } = DidactDefaults.DefaultFlowVersion;

        public string TypeName { get; private set; }

        public string QueueType { get; private set; } = QueueTypes.HyperQueue;

        public string QueueName { get; private set; } = DidactDefaults.DefaultQueueName;

        public IEnumerable<ICronScheduleTrigger> CronScheduleTriggers { get; private set; }

        public TimeSpan? Delay { get; private set; } = null;

        public FlowConfigurator(ILogger<FlowConfigurator> logger, IFlowRepository flowRepository)
        {
            _logger = logger;
            _flowRepository = flowRepository;
        }

        public IFlowConfigurator WithName(string name)
        {
            Name = name;
            return this;
        }

        public IFlowConfigurator WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public IFlowConfigurator AsVersion(string version)
        {
            Version = version;
            return this;
        }

        public IFlowConfigurator WithTypeName(string typeName)
        {
            TypeName = typeName;
            return this;
        }

        public IFlowConfigurator ForQueue(string queueType, string queueName = DidactDefaults.DefaultQueueName)
        {
            QueueType = queueType;
            QueueName = queueName;
            return this;
        }

        public IFlowConfigurator WithCronScheduleTriggers(IEnumerable<ICronScheduleTrigger> cronScheduleTriggers)
        {
            CronScheduleTriggers = cronScheduleTriggers;
            return this;
        }

        public IFlowConfigurator WithDelay(TimeSpan delay)
        {
            Delay = delay;
            return this;
        }

        public async Task SaveConfigurationsAsync()
        {
            try
            {
                _logger.LogInformation("Saving the Flow configurations to persistent storage...");
                await _flowRepository.SaveConfigurationsAsync(this).ConfigureAwait(false);
                _logger.LogInformation("Flow configurations saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Flow configurations were unable to be saved successfully. See inner exception: {ex}", ex);
                var saveFlowConfigurationsException = new SaveFlowConfigurationsException("Flow configurations were unable to be saved successfully. See inner exception.", ex);
                throw saveFlowConfigurationsException;
            }
        }
    }
}
