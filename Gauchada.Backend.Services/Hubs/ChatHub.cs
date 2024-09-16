using Gauchada.Backend.Model.DTO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Services.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            Console.WriteLine($"Joining chat {chatId}");
        }

        public async Task SendMessageToChat(string chatId, string user, string message)
        {
            try
            {
                await Clients.Group(chatId).SendAsync("ReceiveMessage", user, message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ChatHub: " + ex.Message);
            }

        }

        public async Task LeaveChat(string chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
