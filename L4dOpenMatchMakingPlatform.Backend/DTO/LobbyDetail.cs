namespace L4dOpenMatchMakingPlatform.Backend.DTO
{
    public class LobbyDetail
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string? description { get; set; }

        public string type { get; set; }

        public Guid? owner { get; set; }

        public string? endpoint { get; set; }

        public TeamDetail team1 { get; set; }

        public TeamDetail team2 { get; set; }

        public TeamDetail team3 { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? ended_at { get; set; }
    }
}
