namespace L4dOpenMatchMakingPlatform.Backend.Models
{
    public class CustomGameModel
    {
        public Guid lobby_id { get; set; }

        public string lobby_name { get;  set; }

        public string? lobby_description { get; set; }

        public string lobby_type { get; set; }

        public Guid? lobby_owner { get; set; }

        public string? lobby_server_endpoint { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? ended_at { get; set; }

        public TeamModel team1 { get; set; }

        public TeamModel team2 { get; set; }

        public TeamModel team3 { get; set; }
    }
}
