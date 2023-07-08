using System.ComponentModel.DataAnnotations;

namespace L4dOpenMatchMakingPlatform.Backend.Models
{
    public class CustomGameModel
    {
        [Key]
        public Guid lobby_id { get; set; }

        public string lobby_name { get;  set; }

        public string? lobby_description { get; set; }

        public LobbyStatus lobby_status { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? ended_at { get; set; }


        public PlayerModel lobby_owner { get; set; }

        public ServerEndpointModel? server_bound { get; set; }

        public ICollection<TeamModel>? teams { get; set; }

        public GameModeModel mode { get; set; }
    }

    public enum LobbyStatus
    {
        Idle,
        InGame,
    }
}
