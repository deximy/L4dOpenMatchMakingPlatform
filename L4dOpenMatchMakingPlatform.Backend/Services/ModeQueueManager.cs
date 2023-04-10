namespace L4dOpenMatchMakingPlatform.Backend.Services
{
    public static class ModeQueueManager
    {
        public static void UseModeQueueManager(this IEndpointRouteBuilder endpoint_route_builder)
        {
            endpoint_route_builder.UseHunter1v1Queue();
        }
    }
}
