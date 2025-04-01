using Microsoft.AspNetCore.SignalR;

namespace eStore
{
    public class OrderHub : Hub
    {
        public const string HubUrl = "/orderHub"; // Đường dẫn endpoint cho hub

        public async Task BroadcastOrderDelete()
        {
            await Clients.All.SendAsync("ReceiveOrderDelete"); // Gửi thông báo tới tất cả client
        }
        public async Task NotifyOrderUpdated()
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate");
        }
        // Bạn có thể thêm các method khác nếu cần, ví dụ như thông báo cập nhật hoặc xóa sản phẩm

        public override Task OnConnectedAsync()
        {
            System.Console.WriteLine($"{Context.ConnectionId} connected to Order Hub");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            System.Console.WriteLine($"Disconnected {exception?.Message} {Context.ConnectionId} from Order Hub");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
