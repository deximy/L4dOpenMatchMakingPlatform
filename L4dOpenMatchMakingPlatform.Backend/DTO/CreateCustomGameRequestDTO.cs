namespace L4dOpenMatchMakingPlatform.Backend.DTO
{
    public class CreateCustomGameRequestDTO
    {
        public string? lobby_name {  get; set; }

        public string? description { get; set; }

        public string lobby_type { get; set; }
    }
}
