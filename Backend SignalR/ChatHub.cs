using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API
{
    public class ChatHub : Hub
    {
        public async Task AddToGroup(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).SendAsync("ShowConnected", $"{Context.ConnectionId} has joined");
        }
        public async Task SendMessage(string room, string message, string name, string date)
        {
            await Clients.Group(room).SendAsync("Recive", message, name, date);
        }
    }
}
