using Catalog.API.Models.Dtos;

namespace Catalog.API.Services.Implementations
{
    public class AgentService : IAgentService
    {
        private readonly IPythonServiceClient _pythonServiceClient;

        public AgentService(IPythonServiceClient pythonServiceClient)
        {
            _pythonServiceClient = pythonServiceClient;
        }

        public async Task<AgentQueryResponse> TestAgent()
        {
            var request = new AgentQueryRequest { Query = "文档3的更新日期是" };
            var response = await _pythonServiceClient.CallPythonApiAsync<AgentQueryRequest, AgentQueryResponse>(
                "/api/test/agent/query",
                request
            );
            return response;
        }
    }
}
