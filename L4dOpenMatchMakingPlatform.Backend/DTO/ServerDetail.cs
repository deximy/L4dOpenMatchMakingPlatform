namespace L4dOpenMatchMakingPlatform.Backend.DTO
{
    public class ServerDetail
    {
        public Guid id { get; set; }

        public string endpoint { get; set; }

        public string? name { get; set; }

        public Guid? owner_id { get; set; }

        public ICollection<string> supported_mode { get; set; }
    }
}
