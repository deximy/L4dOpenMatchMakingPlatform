namespace L4dOpenMatchMakingPlatform.Backend.DTO
{
    public class LobbySummary
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public TeamSummary team1 { get; set; }

        public TeamSummary team2 { get; set; }

        public TeamSummary team3 { get; set; }
    }
}
