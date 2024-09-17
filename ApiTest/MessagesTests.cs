using Gauchada.Backend.Data;
using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gauchada.Backend.ApiTest
{
    public class MessagesTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private AppDbContext _dbContext;
        private Mock<MessageRepository> _messageRepository;
        private Mock<ChatRepository> _chatRepository;

        public MessagesTests() 
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DriverTestDatabase")
                .Options;
            _dbContext = new AppDbContext(_dbContextOptions);
            _messageRepository = new Mock<MessageRepository>(_dbContext);
            _chatRepository = new Mock<ChatRepository>(_dbContext);
        }

        [Fact]
        public async Task ChatRepository_ReturnsChatMessages_WhenCorrect()
        {
            // Arrange
            var chat = new Chat { ChatId = 2, Active = true, TripId = 1, Messages = new List<Message>() };
            _dbContext.Chats.Add(chat);
            _dbContext.SaveChanges();

            await _messageRepository.Object.CreateMessage(1, "hola", "Writer");


            // Act  
            var result = await _chatRepository.Object.ReturnChatMessages(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("hola", result.First().MessageContent);
            Assert.Equal("Writer", result.First().WriterUsername);
        }
    }
}
