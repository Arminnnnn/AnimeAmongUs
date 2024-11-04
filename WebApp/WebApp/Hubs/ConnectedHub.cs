using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs;

public class ConnectedHub : Hub
{
    private static int _connectionCount;
    private static bool _buttonState;
    
    public override Task OnConnectedAsync()
    {
        _connectionCount++;
        Clients.All.SendAsync("OnConnected", _connectionCount, _buttonState);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connectionCount--;
        Clients.All.SendAsync("OnConnected", _connectionCount, _buttonState);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task ButtonClicked()
    {
        _buttonState = true;
        Clients.All.SendAsync("OnConnected", _connectionCount, _buttonState);
    }

    public async Task ButtonUndo()
    {
        _buttonState = false;
        Clients.All.SendAsync("OnConnected", _connectionCount, _buttonState);
    }
}