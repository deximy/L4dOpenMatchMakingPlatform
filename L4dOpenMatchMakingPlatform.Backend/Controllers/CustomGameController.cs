using L4dOpenMatchMakingPlatform.Backend.Services;
using L4dOpenMatchMakingPlatform.Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using L4dOpenMatchMakingPlatform.Backend.DTO;

namespace L4dOpenMatchMakingPlatform.Backend
{
    [Route("apis/custom-games")]
    [ApiController]
    public class CustomGameController : ControllerBase
    {
        private readonly CustomGameService custom_game_service_;
        private readonly BindService bind_service_;

        public CustomGameController(CustomGameService custom_game_manager, BindService bind_service)
        {
            custom_game_service_ = custom_game_manager;
            bind_service_ = bind_service;
        }

        [HttpGet]
        public IActionResult QueryLobbySummaryList([FromQuery] int page_index, [FromQuery] int page_size, [FromQuery] bool? count)
        {
            var lobby_list = custom_game_service_.GetCustomGamesByPage(page_index, page_size);
            var lobby_summary_list = new List<LobbySummary>();
            foreach (var lobby in lobby_list)
            {
                lobby_summary_list.Add(
                    new LobbySummary() {
                        id = lobby.lobby_id,
                        name = lobby.lobby_name,
                        type = lobby.lobby_type,
                        team1 = new TeamSummary() {
                            current_player_count = lobby.team1.current_players.Count,
                            max_player_count = lobby.team1.max_player_count,
                        },
                        team2 = new TeamSummary() {
                            current_player_count = lobby.team2.current_players.Count,
                            max_player_count = lobby.team2.max_player_count,
                        },
                        team3 = new TeamSummary() {
                            current_player_count = lobby.team3.current_players.Count,
                            max_player_count = lobby.team3.max_player_count,
                        },
                    }
                );
            }
            return Ok(
                new QueryLobbySummaryListResponseDTO() {
                    lobby_summary_list = lobby_summary_list,
                    page_index = page_index,
                    page_size = page_size,
                    page_count = (int)Math.Ceiling((decimal)custom_game_service_.GetCustomGamesCount() / page_size),
                    total_count = custom_game_service_.GetCustomGamesCount(),
                }
            );
        }

        [HttpGet("{lobby_id}")]
        public IActionResult QueryLobbyDetail([FromRoute] Guid lobby_id)
        {
            /*
            var properties = lobby.GetType().GetProperties();
            var selected_properties = properties.IntersectBy(filter_array.Distinct(), property => property.Name);
            var result_dictionary = new Dictionary<string, object>();
            foreach (var property in selected_properties)
            {
                result_dictionary.Add(property.Name, property.GetValue(lobby));
            }
            */
            var lobby = custom_game_service_.GetGustomGame(lobby_id);
            var lobby_detail = new LobbyDetail() {
                id = lobby_id,
                name = lobby.lobby_name,
                description = lobby.lobby_description,
                type = lobby.lobby_type,
                owner = lobby.lobby_owner,
                server = lobby.lobby_bound_server == null ? null : new ServerDetail() {
                    id = lobby.lobby_bound_server.id,
                    endpoint = lobby.lobby_bound_server.endpoint,
                    name = lobby.lobby_bound_server.name,
                    owner_id = lobby.lobby_bound_server.owner_id,
                    supported_mode = lobby.lobby_bound_server.supported_mode,
                },
                team1 = new TeamDetail() {
                    id = lobby.team1.team_id,
                    name = lobby.team1.team_name,
                    players = lobby.team1.current_players,
                    max_player_count = lobby.team1.max_player_count,
                },
                team2 = new TeamDetail()
                {
                    id = lobby.team2.team_id,
                    name = lobby.team2.team_name,
                    players = lobby.team2.current_players,
                    max_player_count = lobby.team2.max_player_count,
                },
                team3 = new TeamDetail()
                {
                    id = lobby.team3.team_id,
                    name = lobby.team3.team_name,
                    players = lobby.team3.current_players,
                    max_player_count = lobby.team3.max_player_count,
                },
                created_at = lobby.created_at,
                ended_at = lobby.ended_at,
            };
            return Ok(lobby_detail);
        }

        [HttpPost]
        public IActionResult CreateCustomGame([FromBody] CreateCustomGameRequestDTO dto)
        {
            var new_custom_game = custom_game_service_.CreateCustomGame(
                dto.lobby_type,
                dto.lobby_owner_id,
                dto.lobby_name,
                dto.description
            );
            var lobby_detail = new LobbyDetail() {
                id = new_custom_game.lobby_id,
                name = new_custom_game.lobby_name,
                description = new_custom_game.lobby_description,
                type = new_custom_game.lobby_type,
                owner = new_custom_game.lobby_owner,
                server = null,
                team1 = new TeamDetail() {
                    id = new_custom_game.team1.team_id,
                    name = new_custom_game.team1.team_name,
                    players = new_custom_game.team1.current_players,
                    max_player_count = new_custom_game.team1.max_player_count,
                },
                team2 = new TeamDetail()
                {
                    id = new_custom_game.team2.team_id,
                    name = new_custom_game.team2.team_name,
                    players = new_custom_game.team2.current_players,
                    max_player_count = new_custom_game.team2.max_player_count,
                },
                team3 = new TeamDetail()
                {
                    id = new_custom_game.team3.team_id,
                    name = new_custom_game.team3.team_name,
                    players = new_custom_game.team3.current_players,
                    max_player_count = new_custom_game.team3.max_player_count,
                },
                created_at = new_custom_game.created_at,
                ended_at = new_custom_game.ended_at,
            };
            return Created(
                new_custom_game.lobby_id.ToString(),
                lobby_detail
            );
        }

        [HttpPatch("{lobby_id}")]
        public IActionResult UpdateProperties([FromRoute] Guid lobby_id, [FromBody] UpdatePropertiesRequestDTO dto)
        {
            {
                if (dto.properties.TryGetValue("lobby_name", out string? result) && result != string.Empty)
                {
                    custom_game_service_.UpdateCustomGameName(lobby_id, result);
                }
            }
            {
                if (dto.properties.TryGetValue("lobby_description", out string? result))
                {
                    custom_game_service_.UpdateCustomGameDdescription(lobby_id, result);
                }
            }
            {
                if (dto.properties.TryGetValue("lobby_type", out string? result) && result != string.Empty)
                {
                    custom_game_service_.UpdateCustomGameType(lobby_id, result);
                }
            }
            {
                if (dto.properties.TryGetValue("lobby_owner", out string? result))
                {
                    custom_game_service_.UpdateCustomGameOwner(lobby_id, result == null ? null : Guid.Parse(result));
                }
            }
            {
                if (dto.properties.TryGetValue("lobby_server_endpoint", out string? result))
                {
                    bind_service_.BindServerAndLobby(Guid.Parse(result), lobby_id);
                }
            }

            return Ok();
        }

        [HttpPost("{lobby_id}/teams/{team_id}/players/{player_id}")]
        public IActionResult HandlePlayerJoinLobby([FromRoute] Guid lobby_id, [FromRoute] Guid team_id, [FromRoute] Guid player_id)
        {
            var lobby = custom_game_service_.GetGustomGame(lobby_id);
            if (lobby.team1.team_id == team_id)
            {
                lobby.team1.current_players.Add(player_id);
            }
            else if (lobby.team2.team_id == team_id)
            {
                lobby.team2.current_players.Add(player_id);
            }
            else if (lobby.team3.team_id == team_id)
            {
                lobby.team3.current_players.Add(player_id);
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{lobby_id}/teams/{team_id}/players/{player_id}")]
        public IActionResult HandlePlayerLeaveLobby([FromRoute] Guid lobby_id, [FromRoute] Guid team_id, [FromRoute] Guid player_id)
        {
            var lobby = custom_game_service_.GetGustomGame(lobby_id);
            if (lobby.team1.team_id == team_id)
            {
                lobby.team1.current_players.Remove(player_id);
            }
            else if (lobby.team2.team_id == team_id)
            {
                lobby.team2.current_players.Remove(player_id);
            }
            else if (lobby.team3.team_id == team_id)
            {
                lobby.team3.current_players.Remove(player_id);
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
