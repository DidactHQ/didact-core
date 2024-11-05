﻿using DidactCore.Engine;
using DidactCore.Plugins;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public class FlowExecutor : IFlowExecutor
    {
        private readonly IEngineSupervisor _engineSupervisor;
        private readonly IFlowRunRepository _flowRunRepository;
        private readonly ILogger<IFlowExecutor> _logger;
        private readonly int _flowRunCancellationCheckInterval;

        public FlowExecutor(ILogger<IFlowExecutor> logger, IEngineSupervisor engineSupervisor, IFlowRunRepository flowRunRepository)
        {
            _engineSupervisor = engineSupervisor ?? throw new ArgumentNullException(nameof(engineSupervisor));
            _flowRunRepository = flowRunRepository ?? throw new ArgumentNullException(nameof(flowRunRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _flowRunCancellationCheckInterval = engineSupervisor.EngineTuning.FlowRunCancellationCheckInterval;
        }

        public async Task<FlowRunDto> FetchFlowRunAsync()
        {
            // TODO Implement
            await Task.CompletedTask;
            return new FlowRunDto();
        }

        public async Task<FlowRunDto> CreateFlowInstanceAsync(FlowRunDto flowRunDto)
        {
            IPluginContainer pluginContainer;

            try
            {
                pluginContainer = _engineSupervisor.PluginContainers.FindMatchingPluginContainer(flowRunDto.PluginExecutionVersion);
            }
            catch (NoMatchedPluginException ex)
            {
                throw;
            }

            // Traverse the AppDomain's assemblies to get the type.
            // Remember that .NET 5+ only has 1 AppDomain going forward, so CurrentDomain is sufficient.
            var flowType = pluginContainer.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.Name == flowRunDto.FlowTypeName && t.GetInterfaces().Contains(typeof(IFlow)) && t.IsClass && !t.IsAbstract)
                .SingleOrDefault();

            if (flowType is null)
            {
                throw new FlowTypeNotFoundException();
            }

            // Create an instance of the type using the dependency injection system.
            // Then safe cast to an IFlow.
            var iflow = pluginContainer.PluginDependencyInjector.CreateInstance(flowType) as IFlow
                ?? throw new NullReferenceException();

            // I'm going to leave the method signature async for the moment, so fulfill the signature.
            await Task.CompletedTask;

            flowRunDto.FlowInstance = iflow;
            return flowRunDto;
        }

        public async Task CheckForFlowRunCancellationAsync(long flowRunId, CancellationToken compositeCancellationToken)
        {
            while (true)
            {
                try
                {
                    var isCancelled = await _flowRunRepository.CheckIfFlowRunIsCancelledAsync(flowRunId);
                    if (isCancelled)
                        return;
                    else
                        await Task.Delay(_flowRunCancellationCheckInterval, compositeCancellationToken);
                }
                catch (Exception)
                {
                    _logger.LogError("FlowRun cancellation check failed for FlowRunId {flowRunId}.", flowRunId);
                }
            }
        }

        public async Task ExecuteFlowRunTimeoutCountdownAsync(int flowRunTimeoutSeconds, CancellationToken compositeCancellationToken)
        {
            var flowRunTimeoutMilliseconds = flowRunTimeoutSeconds * 1000;
            await Task.Delay(flowRunTimeoutMilliseconds, compositeCancellationToken);
        } 

        public async Task ExecuteFlowRunAsync(FlowRunDto flowRunDto, CancellationToken compositeCancellationToken)
        {
            if (flowRunDto.FlowInstance is null)
            {
                throw new Exception("Oops.");
            }

            var flowExecutionContext = new FlowExecutionContext(flowRunDto.StringifiedJsonPayload, compositeCancellationToken);
            try
            {
                await flowRunDto.FlowInstance.ExecuteAsync(flowExecutionContext);
            }
            catch (OperationCanceledException)
            {
                await _flowRunRepository.CancelFlowRunAsync(flowRunDto.FlowRunId);
            }
            catch (Exception)
            {
                // TODO
                // Handle retries here.
                // After final failure, log to storage.
                // Do not actually throw;
            }
        }

        public async Task ExecuteIntelligentAsync(FlowRunDto flowRunDto)
        {
            // Create cancellation token for FlowRun cancellations from persistent storage.
            using var flowRunCancellationTokenSource = new CancellationTokenSource();
            // Create cancellation token for FlowRun timeout.
            using var flowRunTimeoutCancellationTokenSource = new CancellationTokenSource();
            // Create a composite cancellation token for engine shutdown, FlowRun cancellation, and FlowRun timeout.
            using var compositeCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(flowRunCancellationTokenSource.Token, flowRunTimeoutCancellationTokenSource.Token, _engineSupervisor.CancellationToken);
            var compositeCancellationToken = compositeCancellationTokenSource.Token;
            
            var flowRunExecutionTask = ExecuteFlowRunAsync(flowRunDto, compositeCancellationToken);
            var flowRunTimeoutTask = ExecuteFlowRunTimeoutCountdownAsync(flowRunDto.TimeoutSeconds, compositeCancellationToken);
            var flowRunCancellationCheckTask = CheckForFlowRunCancellationAsync(flowRunDto.FlowRunId, compositeCancellationToken);

            var completedTask = await Task.WhenAny(flowRunExecutionTask, flowRunCancellationCheckTask, flowRunTimeoutTask);
            if (completedTask == flowRunCancellationCheckTask)
            {
                // TODO Handle cancellation reason.
                flowRunCancellationTokenSource?.Cancel();
                await flowRunExecutionTask;
            }
            else if (completedTask == flowRunTimeoutTask)
            {
                // TODO Handle timeout cancellation reason.
                flowRunTimeoutCancellationTokenSource?.Cancel();
                await flowRunExecutionTask;
            }
        }
    }
}
