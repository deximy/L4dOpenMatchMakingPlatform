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

        public CustomGameController(CustomGameService custom_game_manager)
        {
            custom_game_service_ = custom_game_manager;
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
        public IActionResult QueryLobbySummary([FromRoute] Guid lobby_id)
        {
            var lobby = custom_game_service_.GetGustomGame(lobby_id);
            /*
            var properties = lobby.GetType().GetProperties();
            var selected_properties = properties.IntersectBy(filter_array.Distinct(), property => property.Name);
            var result_dictionary = new Dictionary<string, object>();
            foreach (var property in selected_properties)
            {
                result_dictionary.Add(property.Name, property.GetValue(lobby));
            }
            */
            return Ok(lobby);
        }

        [HttpPost]
        public IActionResult CreateCustomGame([FromBody] CreateCustomGameRequestDTO dto)
        {
            var new_custom_game = custom_game_service_.CreateCustomGame(dto.lobby_type, null, dto.lobby_name, dto.description);
            return Created(
                new_custom_game.lobby_id.ToString(),
                new_custom_game
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
                    custom_game_service_.UpdateCustomGameEndpoint(lobby_id, result);
                }
            }

            return Ok();
        }
    }
}
