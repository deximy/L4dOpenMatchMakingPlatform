using System.ComponentModel.DataAnnotations;

namespace L4dOpenMatchMakingPlatform.Backend.Models
{
    public class GameModeModel
    {
        [Key]
        public Guid id { get; set; }

        public string name { get; set; }


        public ICollection<TeamConfigModel> team_config { get; set; }

        public ICollection<ServerEndpointModel> server_supported { get; set; }
    }
}
