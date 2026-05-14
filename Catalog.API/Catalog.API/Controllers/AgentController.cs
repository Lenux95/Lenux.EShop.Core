using Catalog.API.Models.Api;
using Catalog.API.Models.Dtos;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet("agent")]
        public async Task<AgentQueryResponse> TestAgent([FromQuery] string query)
        {
            return await _agentService.TestAgent(query);
        }
    }
}
