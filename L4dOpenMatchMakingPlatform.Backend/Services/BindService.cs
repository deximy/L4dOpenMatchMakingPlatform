namespace L4dOpenMatchMakingPlatform.Backend.Services
{
    public class BindService
    {
        private readonly CustomGameService custom_game_service_;
        private readonly ServerEndpointService server_endpoint_service_;

        public BindService(CustomGameService custom_game_service, ServerEndpointService server_endpoint_service)
        {
            custom_game_service_ = custom_game_service;
            server_endpoint_service_ = server_endpoint_service;
        }

        public bool BindServerAndLobby(Guid server_id, Guid lobby_id)
        {
            var server = server_endpoint_service_.GetEndpointById(server_id);
            var lobby = custom_game_service_.GetGustomGame(lobby_id);
            if (server.server_bound_lobby != null || lobby.lobby_bound_server != null)
            {
                return false;
            }

            server_endpoint_service_.BindLobbyToEndpoint(server, lobby);
            custom_game_service_.BindEndpointToLobby(lobby, server);
            return true;
        }
    }
}
