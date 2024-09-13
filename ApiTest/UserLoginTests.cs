using Gauchada.Backend.API.Controllers;
using Gauchada.Backend.Data;
using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.ApiTest
{
    public class UserLoginTests : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private AppDbContext _dbContext;
        private Mock<DriverRepository> _driverRepository;
        private Mock<LoginService> _loginService;
        private Mock<UserLoginController> _userLoginController;
        public UserLoginTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserLoginTestDatabase")
                .Options;
            _dbContext = new AppDbContext(_dbContextOptions);
            _driverRepository = new Mock<DriverRepository>(_dbContext);
            _loginService = new Mock<LoginService>(_driverRepository.Object);
            _userLoginController = new Mock<UserLoginController>(_loginService.Object);
        }

        public void Dispose()
        {
        }

        [Fact]
        public async Task DriverLogin_ReturnsToken_WhenPasswordAndUserAreCorrect() {
            // Arrange
            string driverUserName = "existingdriver";
            var driverEntity = new DriverEntity
            {
                UserName = driverUserName,
                Name = "John",
                LastName = "Doe",
                Email = "john@hot.com",
                Birth = new DateTime(DateTime.Now.Year - 20, 1, 1),
                PhoneNumber = "123456789",
                PhotoSrc = "photo.jpg"
            };
            _dbContext.Drivers.Add(driverEntity);
            _dbContext.SaveChanges();

            var result = await _userLoginController.Object.DriverLogin(driverUserName, "123456");
            Assert.Equal(result.Value.Message, "Token Generated");  
        }
    }
}
