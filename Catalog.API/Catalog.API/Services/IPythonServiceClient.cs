namespace Catalog.API.Services
{
    public interface IPythonServiceClient
    {
        Task<TResponse> CallPythonApiAsync<TRequest, TResponse>(string endpoint, TRequest request);
        Task<TResponse> GetFromPythonApiAsync<TResponse>(string endpoint);
    }
}
