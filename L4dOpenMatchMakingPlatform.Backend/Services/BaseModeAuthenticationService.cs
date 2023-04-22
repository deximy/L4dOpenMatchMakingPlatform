using Microsoft.AspNetCore.SignalR;

namespace L4dOpenMatchMakingPlatform.Backend.Services
{
    public class BaseModeAuthenticationService : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.GetHttpContext()?.Request.Query["access_token"] ?? throw new HubException("access_token can't be empty");
        }
    }
}
