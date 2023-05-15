namespace L4dOpenMatchMakingPlatform.Backend.DTO
{
    public class TeamDetail
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public ICollection<Guid> players { get; set; }

        public int max_player_count { get; set; }
    }
}
