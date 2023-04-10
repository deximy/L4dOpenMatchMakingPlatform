namespace L4dOpenMatchMakingPlatform.Backend.Services
{
    public static class Hunter1v1Queue
    {
        public static void UseHunter1v1Queue(this IEndpointRouteBuilder endpoint_route_builder)
        {
            endpoint_route_builder.MapHub<BaseModeEventHandler>("/Hunter1v1");
        }
    }
}
