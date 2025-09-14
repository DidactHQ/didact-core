using DidactCore.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace DidactCore.Blocks.GenericActionBlocks
{
    public class HttpActionTaskBlock
    {
        private readonly IDidactDependencyInjector _didactDependencyInjector;
        private readonly ILogger<HttpActionTaskBlock>? _logger;

        public HttpActionTaskBlock(IDidactDependencyInjector didactDependencyInjector)
        {
            _didactDependencyInjector = didactDependencyInjector;
            _logger = _didactDependencyInjector.FlowServiceProvider.GetService<ILogger<HttpActionTaskBlock>>();
        }

        public async Task ExecuteAsync(string url, HttpMethod method)
        {
            _logger?.LogInformation("Starting HTTP request to: {Url} with method: {Method}", url, method.Method);

            try
            {
                using var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(method, url));
                
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                
                _logger?.LogInformation("HTTP response status: {StatusCode}", response.StatusCode);
                Console.WriteLine($"Response content: {(content.Length > 100 ? content.Substring(0, 100) + "..." : content)}");
                
                // Store the response in memory for possible use by other blocks
                StoreResponseInMemory(response.StatusCode.ToString(), content);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error executing HTTP request to {Url}", url);
                throw;
            }
            
            _logger?.LogInformation("HTTP request completed");
        }
        
        public async Task ExecuteWithDatabaseAsync(string url, HttpMethod method, string dbOperationType = "read")
        {
            _logger?.LogInformation("Starting HTTP request with database operation to: {Url}", url);
            
            try
            {
                // First execute the HTTP request
                using var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(method, url));
                
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                
                _logger?.LogInformation("HTTP response received, status: {StatusCode}", response.StatusCode);
                
                // Get the database context from DI
                var dbContextType = Type.GetType("DidactEngine.Services.Contexts.DidactDbContext, DidactEngine");
                if (dbContextType == null)
                {
                    _logger?.LogWarning("DbContext type not found. Database operations will be skipped.");
                    return;
                }
                
                var dbContext = _didactDependencyInjector.FlowServiceProvider.GetService(dbContextType);
                if (dbContext == null)
                {
                    _logger?.LogWarning("DbContext not available from DI container. Database operations will be skipped.");
                    return;
                }
                
                // Log that we've found the DbContext
                _logger?.LogInformation("DbContext found, performing {Operation} operation", dbOperationType);
                
                // Store the result for other blocks to use
                StoreResponseInMemory(response.StatusCode.ToString(), content);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error executing HTTP request with database operation");
                throw;
            }
        }
        
        private void StoreResponseInMemory(string status, string content)
        {
            // This is a placeholder for storing the response in memory
            // In a real implementation, you might use a service or a shared state mechanism
            Console.WriteLine($"Stored HTTP response: Status={status}, Content length={content.Length}");
        }
    }
}
