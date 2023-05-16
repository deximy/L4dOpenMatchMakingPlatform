using L4dOpenMatchMakingPlatform.Backend.DTO;
using L4dOpenMatchMakingPlatform.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace L4dOpenMatchMakingPlatform.Backend.Controllers
{
    [Route("endpoints")]
    [ApiController]
    public class ServerEndpointController : ControllerBase
    {
        private readonly ServerEndpointService endpoint_service_;

        public ServerEndpointController(ServerEndpointService endpoint_service)
        {
            endpoint_service_ = endpoint_service;
        }

        [HttpGet("{mode_name}")]
        public IActionResult GetModeServers([FromRoute] string mode_name)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult RegisterNewServer([FromBody] CreateServerEndpointRequestDTO dto)
        {
            var new_endpoint = endpoint_service_.CreateServerEndpoint(dto.endpoint, dto.owner_id, dto.name, dto.supported_mode);
            return Ok(new_endpoint);
        }
    }
}
