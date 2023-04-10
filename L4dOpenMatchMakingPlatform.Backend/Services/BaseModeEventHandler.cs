using L4dOpenMatchMakingPlatform.Backend.ViewModels;
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
            Console.WriteLine($"OnConnectedAsync triggered. Connection ID: {Context.ConnectionId}");
            await base.OnConnectedAsync();
            await HandleQueueConnected(Context.ConnectionId);
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"OnDisconnectedAsync. Connection ID: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
            await HandleQueueDisconnected(Context.ConnectionId);
        }

        public async virtual Task HandleQueueConnected(string connection_id)
        {
            client_list_.Add(connection_id);
            await Clients.All.SendCoreAsync(
                "OnQueueConnected",
                new ModeQueueChangedViewModel[] {
                    new ModeQueueChangedViewModel() {
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
                new ModeQueueChangedViewModel[] {
                    new ModeQueueChangedViewModel() {
                        id = connection_id,
                        count = client_list_.Count
                    }
                }
            );
        }
    }
}
