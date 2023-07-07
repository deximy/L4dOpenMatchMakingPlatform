using System.ComponentModel.DataAnnotations;

namespace L4dOpenMatchMakingPlatform.Backend.Models
{
    public class TeamConfigModel
    {
        [Key]
        public Guid id { get; set; }

        public string default_name { get; set; }

        public int max_player_count { get; set; }
    }
}
