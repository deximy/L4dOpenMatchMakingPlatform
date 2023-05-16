using L4dOpenMatchMakingPlatform.Backend.Models;

namespace L4dOpenMatchMakingPlatform.Backend.Repositories
{
    public class ServerEndpointRepository
    {
        private IList<ServerEndpointModel> endpoint_list_;

        public ServerEndpointRepository()
        {
            endpoint_list_ = new List<ServerEndpointModel>();
        }

        public ServerEndpointModel Create(ServerEndpointModel new_server_endpoint)
        {
            endpoint_list_.Add(new_server_endpoint);
            return endpoint_list_.First(i => i.id == new_server_endpoint.id);
        }
    }
}
