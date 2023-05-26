using L4dOpenMatchMakingPlatform.Backend.Models;
using L4dOpenMatchMakingPlatform.Backend.Repositories;

namespace L4dOpenMatchMakingPlatform.Backend.Services
{
    public class ServerEndpointService
    {
        private readonly ServerEndpointRepository endpoint_repository_;

        public ServerEndpointService(ServerEndpointRepository endpoint_repository)
        {
            endpoint_repository_ = endpoint_repository;
        }

        public ServerEndpointModel CreateServerEndpoint(string endpoint, Guid? owner_id, string? name, ICollection<string> supported_mode)
        {
            var new_server_endpoint = new ServerEndpointModel() {
                id = Guid.NewGuid(),
                endpoint = endpoint,
                owner_id = owner_id,
                name = name,
                supported_mode = supported_mode,
            };
            return endpoint_repository_.Create(new_server_endpoint);
        }

        public IEnumerable<ServerEndpointModel> GetEndpointsForGameMode(string mode_name)
        {
            return endpoint_repository_.Query(x => x.supported_mode.Contains(mode_name));
        }
    }
}
