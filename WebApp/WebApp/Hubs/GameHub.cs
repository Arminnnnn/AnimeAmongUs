using Microsoft.AspNetCore.SignalR;
using WebApp.Services;

namespace WebApp.Hubs;

public class GameHub : Hub
{
    private static List<string> _connectedPlayers = new();

    public override Task OnConnectedAsync()
    {
        _connectedPlayers.Add(Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connectedPlayers.Remove(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
    
    public async Task StartGame()
    {
        this.EmtpyGroups();
        GameService _gameService = new(_connectedPlayers);
        
        var imposters = _gameService.GetImposters();
        
        foreach (var player in _connectedPlayers)
        {
            if (imposters.Contains(player))
            {
                await Groups.AddToGroupAsync(player, "Imposters");
            }
            else
            {
                await Groups.AddToGroupAsync(player, "Innocents");
            }
        }
        
        await Clients.Group("Imposters").SendAsync("ReceiveCharacter", await _gameService.GetImposterCharacter());
        await Clients.Group("Innocents").SendAsync("ReceiveCharacter", await _gameService.GetInnocentCharacter());
    }

    private async Task EmtpyGroups()
    {
        foreach (var player in _connectedPlayers)
        {
           await Groups.RemoveFromGroupAsync(player, "Imposters");
           await Groups.RemoveFromGroupAsync(player, "Innocents");
        }
    }
}