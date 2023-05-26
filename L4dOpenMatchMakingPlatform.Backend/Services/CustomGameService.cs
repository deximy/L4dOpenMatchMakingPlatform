using L4dOpenMatchMakingPlatform.Backend.Models;
using L4dOpenMatchMakingPlatform.Backend.Repositories;

namespace L4dOpenMatchMakingPlatform.Backend.Services
{
    public class CustomGameService
    {
        private readonly CustomGameRepository custom_game_repository_;

        public CustomGameService(CustomGameRepository custom_game_repository)
        {
            custom_game_repository_ = custom_game_repository;
        }

        public CustomGameModel CreateCustomGame(string lobby_type, Guid? lobby_owner, string? lobby_name, string? lobby_description)
        {
            var new_custom_game = new CustomGameModel() {
                lobby_id = Guid.NewGuid(),
                lobby_name = lobby_name ?? "默认",
                lobby_description = lobby_description,
                lobby_owner = lobby_owner,
                lobby_type = lobby_type,
                lobby_bound_server = null,
                created_at = DateTime.UtcNow,
                ended_at = null,
                team1 = new TeamModel() {
                    team_id = Guid.NewGuid(),
                    team_name = "Team1",
                    max_player_count = 4,
                    current_players = new List<Guid>()
                },
                team2 = new TeamModel() {
                    team_id = Guid.NewGuid(),
                    team_name = "Team2",
                    max_player_count = 4,
                    current_players = new List<Guid>()
                },
                team3 = new TeamModel() {
                    team_id = Guid.NewGuid(),
                    team_name = "Team3",
                    max_player_count = 4,
                    current_players = new List<Guid>()
                },
            };
            return custom_game_repository_.Create(new_custom_game);
        }

        public CustomGameModel GetGustomGame(Guid lobby_id)
        {
            return custom_game_repository_.Query(lobby_id);
        }

        public int GetCustomGamesCount()
        {
            return custom_game_repository_.Query(x => true).Count();
        }

        public IEnumerable<CustomGameModel> GetCustomGamesByPage(int page_index, int page_size = 20)
        {
            return custom_game_repository_.Query(x => true, page_index, page_size);
        }

        public CustomGameModel UpdateCustomGameName(Guid lobby_id, string name)
        {
            var lobby = custom_game_repository_.Query(lobby_id);
            lobby.lobby_name = name;
            return custom_game_repository_.Update(lobby);
        }

        public CustomGameModel UpdateCustomGameDdescription(Guid lobby_id, string? description)
        {
            var lobby = custom_game_repository_.Query(lobby_id);
            lobby.lobby_description = description;
            return custom_game_repository_.Update(lobby);
        }

        public CustomGameModel UpdateCustomGameType(Guid lobby_id, string type)
        {
            var lobby = custom_game_repository_.Query(lobby_id);
            lobby.lobby_type = type;
            return custom_game_repository_.Update(lobby);
        }

        public CustomGameModel UpdateCustomGameOwner(Guid lobby_id, Guid? owner)
        {
            var lobby = custom_game_repository_.Query(lobby_id);
            lobby.lobby_owner = owner;
            return custom_game_repository_.Update(lobby);
        }

        public CustomGameModel BindEndpointToLobby(CustomGameModel lobby_model, ServerEndpointModel endpoint_model)
        {
            lobby_model.lobby_bound_server = endpoint_model;
            return custom_game_repository_.Update(lobby_model);
        }
    }
}
