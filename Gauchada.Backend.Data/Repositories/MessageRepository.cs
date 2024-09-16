using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _dbContext;
        public MessageRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task CreateMessage(int chatId, string message, string writer, string userType)
        {
            try
            {
                if (userType == "passenger")
                {
                    _dbContext.Messages.Add(new Message { ChatId = chatId, MessageContent = message, WriterUsername = writer, WriteTime = DateTime.Now });
                }
                else if (userType == "driver")
                {
                    _dbContext.DriverMessages.Add(new DriverMessage { ChatId = chatId, MessageContent = message, WriterUsername = writer, WriteTime = DateTime.Now });
                }
                else throw new Exception("userType incorrect");
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
    }
}
