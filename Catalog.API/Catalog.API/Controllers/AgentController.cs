using Catalog.API.Models.Dtos;
using Catalog.API.Services;
using Catalog.API.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentController:ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService= agentService;
        }

        [HttpGet("agent")]
        public async Task<ActionResult<AgentQueryResponse>> TestAgent()
        {
            var respose = await _agentService.TestAgent();
            return Ok(respose);
        }
    }
}
