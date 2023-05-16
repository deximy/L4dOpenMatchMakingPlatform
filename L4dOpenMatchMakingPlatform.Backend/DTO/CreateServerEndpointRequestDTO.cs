namespace L4dOpenMatchMakingPlatform.Backend.DTO
{
    public class CreateServerEndpointRequestDTO
    {
        public string endpoint { get; set; }

        public string? name { get; set; }

        public ICollection<string> supported_mode { get; set; }

        public Guid owner_id { get; set; }
    }
}
