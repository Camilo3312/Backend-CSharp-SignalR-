using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Backend_SignalR
{
    public class NotificationHub : Hub
    {
        public async Task ConnectionNotf(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await Clients.Group(userId).SendAsync("ShowConnected", $"{Context.ConnectionId} has joined");
        }
        public async Task SendNotification(string userId, string message, string username, string color, string imageProfile)
        {
            await Clients.Group(userId).SendAsync("ReciveMessage", userId, message, username, color, imageProfile);
        }
    }
}
