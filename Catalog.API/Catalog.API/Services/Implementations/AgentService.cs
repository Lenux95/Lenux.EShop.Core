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

        public async Task<AgentQueryResponse> TestAgent(string query)
        {
            //var request = new AgentQueryRequest { Query = "文档3的更新日期是" };
            var request = new AgentQueryRequest { Query = query };
            var response = await _pythonServiceClient.CallPythonApiAsync<AgentQueryRequest, AgentQueryResponse>(
                "/api/test/agent/eshop_query",
                request
            );
            return response;
        }
    }
}
