using L4dOpenMatchMakingPlatform.Backend.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace L4dOpenMatchMakingPlatform.Backend.Services
{
    public class BaseModeEventHandler : Hub
    {
        private readonly List<string> client_list_;

        public BaseModeEventHandler()
        {
            client_list_ = new();
        }

        public async override Task OnConnectedAsync()
        {
            Console.WriteLine($"OnConnectedAsync triggered. User Identifier: {Context.UserIdentifier}");
            try
            {
                var user_id = Guid.Parse(Context.UserIdentifier ?? string.Empty);
                await base.OnConnectedAsync();
                await HandleQueueConnected(user_id.ToString());
            }
            catch (Exception exception)
            {
                throw new HubException(exception.Message);
            }
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"OnDisconnectedAsync. User Identifier: {Context.UserIdentifier}");
            try
            {
                var user_id = Guid.Parse(Context.UserIdentifier ?? string.Empty);
                await base.OnDisconnectedAsync(exception);
                await HandleQueueDisconnected(user_id.ToString());
            }
            catch
            {
                throw new HubException(exception?.Message);
            }
        }

        public async virtual Task HandleQueueConnected(string connection_id)
        {
            client_list_.Add(connection_id);
            await Clients.All.SendCoreAsync(
                "OnQueueConnected",
                new ModeQueueChangedDTO[] {
                    new ModeQueueChangedDTO() {
                        id = connection_id,
                        count = client_list_.Count
                    }
                }
            );
        }

        public async virtual Task HandleQueueDisconnected(string connection_id)
        {
            client_list_.Add(connection_id);
            await Clients.All.SendCoreAsync(
                "OnQueueDisconnected",
                new ModeQueueChangedDTO[] {
                    new ModeQueueChangedDTO() {
                        id = connection_id,
                        count = client_list_.Count
                    }
                }
            );
        }
    }
}
