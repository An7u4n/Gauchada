using Gauchada.Backend.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Data.Repositories.Interfaces
{
    public interface IChatRepository
    {
        public Task<int> GetChatIdByTripIdAsync(int tripId);
        public Task<Chat> ReturnChatMessages(int chatId);
        public Task CreateTripChat(int tripId);
    }
}
