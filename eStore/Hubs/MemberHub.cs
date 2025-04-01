using Microsoft.AspNetCore.SignalR;
using BusinessObject.Models.Member;
using Microsoft.Extensions.Logging;

namespace BusinessObject.Hubs;

public class MemberHub : Hub
{
	private readonly ILogger<MemberHub> _logger;

	public MemberHub(ILogger<MemberHub> logger)
	{
		_logger = logger;
	}

	public async Task SendMemberUpdate(string operation, MemberViewDTO member)
	{
		await Clients.All.SendAsync("ReceiveMemberUpdate");
	}

	public override async Task OnConnectedAsync()
	{
		_logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
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