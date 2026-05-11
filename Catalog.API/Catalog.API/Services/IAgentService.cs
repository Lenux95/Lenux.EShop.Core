using Catalog.API.Models.Dtos;

namespace Catalog.API.Services
{
    public interface IAgentService
    {
        Task<AgentQueryResponse> TestAgent(string query);

    }
}
