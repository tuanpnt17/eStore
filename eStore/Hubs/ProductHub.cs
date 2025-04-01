using Microsoft.AspNetCore.SignalR;
using BusinessObject.Models.Product;
using Microsoft.Extensions.Logging;

namespace BusinessObject.Hubs;

public class ProductHub : Hub
{
	private readonly ILogger<ProductHub> _logger;

	public ProductHub(ILogger<ProductHub> logger)
	{
		_logger = logger;
	}

	public async Task SendProductUpdate(string operation, ProductViewDTO product)
	{
		await Clients.All.SendAsync("ReceiveProductUpdate");
	}

	public override async Task OnConnectedAsync()
	{
		_logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception exception)
	{
		if (exception != null)
		{
			_logger.LogError(exception, "Client disconnected with error: {ConnectionId}", Context.ConnectionId);
		}
		else
		{
			_logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
		}
		await base.OnDisconnectedAsync(exception);
	}
}