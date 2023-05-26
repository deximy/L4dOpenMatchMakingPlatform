namespace L4dOpenMatchMakingPlatform.Backend.Models
{
    public class ServerEndpointModel
    {
        public Guid id { get; set; }

        public string endpoint { get; set; }

        public string? name { get; set; }

        public Guid? owner_id { get; set; }

        public ICollection<string> supported_mode { get; set; }

        public CustomGameModel? server_bound_lobby { get; set; }
    }
}
