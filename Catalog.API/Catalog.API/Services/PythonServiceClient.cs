
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;

namespace Catalog.API.Services
{
    public class PythonServiceClient : IPythonServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PythonServiceClient> _logger;

        public PythonServiceClient(IHttpClientFactory httpClientFactory, ILogger<PythonServiceClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient("PythonApiClient");
            _logger = logger;
        }
        //public PythonServiceClient(HttpClient httpClient, ILogger<PythonServiceClient> logger)
        //{
        //    _httpClient = httpClient;
        //    _logger = logger;
        //}

        public async Task<TResponse> CallPythonApiAsync<TRequest, TResponse>(string endpoint, TRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "调用Python服务失败: {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task<TResponse> GetFromPythonApiAsync<TResponse>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "调用Python服务失败: {Endpoint}", endpoint);
                throw;
            }
        }
    }
}
