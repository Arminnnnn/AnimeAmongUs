using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs;

public class ConnectedHub : Hub
{
    private static int connectionCount = 0;
    
    public override Task OnConnectedAsync()
    {
        connectionCount++;
        Clients.All.SendAsync("OnConnected", connectionCount);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        connectionCount--;
        Clients.All.SendAsync("OnConnected", connectionCount);
        return base.OnDisconnectedAsync(exception);
    }
}