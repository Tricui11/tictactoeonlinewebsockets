using Microsoft.AspNetCore.SignalR;

namespace Task5.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() =>
            Context.ConnectionId;        
    }
}