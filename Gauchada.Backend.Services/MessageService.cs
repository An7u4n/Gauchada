using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Services.Hubs;
using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IHubContext<ChatHub> _chatHub;
        public MessageService(IMessageRepository messageRepository, IChatRepository chatRepository, IHubContext<ChatHub> chatHub)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _chatHub = chatHub;
        }

        public async Task SendMessage(int tripId, string messageContent, string writerUsername, string userType)
        {
            try
            {
                var chatId = await _chatRepository.GetChatIdByTripIdAsync(tripId);
                await _messageRepository.CreateMessage(chatId, messageContent, writerUsername, userType);
                await _chatHub.Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", writerUsername, messageContent, DateTime.Now);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
