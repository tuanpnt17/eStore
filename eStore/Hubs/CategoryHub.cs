using Microsoft.AspNetCore.SignalR;
namespace eStore.Hubs
{
    public class CategoryHub : Hub
    {
        public const string HubUrl = "/categoryHub"; // Đường dẫn endpoint cho hub
        public async Task BroadcastCategoryUpdate()
        {
            await Clients.All.SendAsync("ReceiveCategoryUpdate");
            Console.WriteLine("Broadcasted category update to all clients.");
        }

        public override Task OnConnectedAsync()
        {
            System.Console.WriteLine($"{Context.ConnectionId} connected to Category Hub");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            System.Console.WriteLine($"Disconnected {exception?.Message} {Context.ConnectionId} from Category Hub");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
