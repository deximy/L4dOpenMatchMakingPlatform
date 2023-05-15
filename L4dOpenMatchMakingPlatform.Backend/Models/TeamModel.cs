namespace L4dOpenMatchMakingPlatform.Backend.Models
{
    public class TeamModel
    {
        public Guid team_id { get; set; }

        public string team_name { get; set; }

        public int max_player_count { get; set; }

        public ICollection<Guid> current_players { get; set; }
    }
}
