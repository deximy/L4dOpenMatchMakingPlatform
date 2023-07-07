using System.ComponentModel.DataAnnotations;

namespace L4dOpenMatchMakingPlatform.Backend.Models
{
    public class PlayerModel
    {
        [Key]
        public Guid id { get; set; }

        public PlayerStatus status { get; set; }


        public TeamModel? team { get; set; }

        public ICollection<ServerEndpointModel>? server_owned { get; set; }

        public ICollection<CustomGameModel>? lobby_owned { get; set; }
    }

    public enum PlayerStatus
    {
        Idle,
        InQueue,
        InLobby,
        InGame
    }
}
