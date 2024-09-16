using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _dbContext;

        public ChatRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetChatIdByTripIdAsync(int tripId)
        {
            try {
                var chat = await _dbContext.Chats.FirstOrDefaultAsync(c => c.TripId == tripId);

                if (chat != null)
                {
                    return chat.ChatId;
                } else throw new Exception("Chat not found");
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }

        public async Task CreateTripChat(int tripId)
        {
            try
            {
                _dbContext.Chats.Add(new Chat { TripId = tripId, Active = true });
                await _dbContext.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }

        public async Task<Chat> ReturnChatMessages(int tripId)
        {
            try
            {
                return await _dbContext.Chats.Include(c => c.Messages).Include(c => c.DriverMessages).FirstAsync(c => c.TripId == tripId);
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }
    }
}
