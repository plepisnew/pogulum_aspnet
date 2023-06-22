using Microsoft.AspNetCore.SignalR;

namespace Pogulum.Server.Hubs;

public class ChatHub : Hub
{
    public async void SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}