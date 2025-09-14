using DidactCore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DidactCore.Blocks.GenericActionBlocks
{
    public class WebhookActionTaskBlock
    {
        private readonly IDidactDependencyInjector _didactDependencyInjector;
        private readonly ILogger<WebhookActionTaskBlock>? _logger;

        public WebhookActionTaskBlock(IDidactDependencyInjector didactDependencyInjector)
        {
            _didactDependencyInjector = didactDependencyInjector;
            _logger = _didactDependencyInjector.FlowServiceProvider.GetService<ILogger<WebhookActionTaskBlock>>();
        }

        public async Task ExecuteAsync(string? url = null, HttpMethod? method = null, string operation = "get", string entityType = "flow", int? entityId = null, string? payload = null)
        {
            _logger?.LogInformation("Starting WebhookActionTaskBlock execution");
            
            try
            {
                // Handle HTTP webhook functionality if URL is provided
                if (!string.IsNullOrEmpty(url))
                {
                    await HandleWebhookAsync(url, method ?? HttpMethod.Get, payload);
                }
                
                // Handle database operation
                await HandleDatabaseOperationAsync(operation, entityType, entityId, payload);
                
                _logger?.LogInformation("WebhookActionTaskBlock execution completed");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in WebhookActionTaskBlock");
                throw;
            }
        }

        private async Task HandleWebhookAsync(string url, HttpMethod method, string? payload)
        {
            _logger?.LogInformation("Performing webhook request to {Url} with method {Method}", url, method.Method);
            
            try
            {
                using var client = new HttpClient();
                HttpResponseMessage response;
                
                if (method == HttpMethod.Get)
                {
                    response = await client.GetAsync(url);
                }
                else if (method == HttpMethod.Post)
                {
                    var content = new StringContent(payload ?? "{}", Encoding.UTF8, "application/json");
                    response = await client.PostAsync(url, content);
                }
                else if (method == HttpMethod.Put)
                {
                    var content = new StringContent(payload ?? "{}", Encoding.UTF8, "application/json");
                    response = await client.PutAsync(url, content);
                }
                else if (method == HttpMethod.Delete)
                {
                    response = await client.DeleteAsync(url);
                }
                else
                {
                    throw new NotSupportedException($"HTTP method {method.Method} is not supported");
                }
                
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger?.LogInformation("Webhook response: {StatusCode}, Content length: {ContentLength}", 
                    response.StatusCode, responseContent.Length);
                
                Console.WriteLine($"Webhook response: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error performing webhook request to {Url}", url);
                throw;
            }
        }

        private async Task HandleDatabaseOperationAsync(string operation, string entityType, int? entityId, string? payload)
        {
            _logger?.LogInformation("Database operation: {Operation} on {EntityType} with ID {EntityId}", 
                operation, entityType, entityId?.ToString() ?? "null");
            
            // Simulate database work
            await Task.Delay(100);
            
            // Log what would happen in a real implementation
            switch (operation.ToLower())
            {
                case "get":
                    _logger?.LogInformation("Would get {EntityType} with ID {EntityId}", entityType, entityId);
                    break;
                case "create":
                    _logger?.LogInformation("Would create {EntityType} with payload length {PayloadLength}", 
                        entityType, payload?.Length ?? 0);
                    break;
                case "update":
                    _logger?.LogInformation("Would update {EntityType} with ID {EntityId}", entityType, entityId);
                    break;
                case "delete":
                    _logger?.LogInformation("Would delete {EntityType} with ID {EntityId}", entityType, entityId);
                    break;
                default:
                    _logger?.LogWarning("Unknown operation: {Operation}", operation);
                    break;
            }
            
            Console.WriteLine($"Database operation completed: {operation} {entityType}");
        }
    }
}
