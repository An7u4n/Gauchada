using Gauchada.Backend.API.Controllers;
using Gauchada.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Gauchada.Backend.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Services;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Model.DTO;

namespace Gauchada.Backend.ApiTest
{
    public class DriversControllerTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private readonly AppDbContext _dbContext;
        private readonly Mock<DriverRepository> _mockDriverRepository;
        private readonly Mock<DriverService> _driverService;
        private readonly DriversController _controller;
        public DriversControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            _dbContext = new AppDbContext(_dbContextOptions);

            _mockDriverRepository = new Mock<DriverRepository>(_dbContext);
            _driverService = new Mock<DriverService>(_mockDriverRepository.Object);
            _controller = new DriversController(_driverService.Object);
        }

        [Fact]
        public async Task GetDriverByUserName_ReturnsNotFound_WhenDriverDoesNotExist()
        {
            // Arrange
            string driverUserName = "nonexistentdriver";

            // Act
            var result = await _controller.GetDriverByUserName(driverUserName);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var response = Assert.IsType<ControllerResponse>(notFoundResult.Value);
            Assert.Equal("Driver Not Found", response.Message);
        }

        [Fact]
        public async Task GetDriverByUserName_ReturnsOkAndData_WhenDriverExists()
        {
            // Arrange
            string driverUserName = "existingdriver";
            var driverEntity = new DriverEntity
            {
                UserName = driverUserName,
                Name = "Miguel",
                LastName = "Gonzalez",
                Email = "miguel@hotmail.com",
                Birth = new DateTime(1990, 1, 1),
                PhoneNumber = "123456789"
            };
            _dbContext.Drivers.Add(driverEntity);
            _dbContext.SaveChanges();

            var driverDTO = new UserDTO(
                 driverEntity.UserName,
                 driverEntity.Name,
                 driverEntity.LastName,
                 driverEntity.Email,
                 driverEntity.Birth,
                 driverEntity.PhoneNumber
             );

            // Act
            var result = await _controller.GetDriverByUserName(driverUserName);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<ControllerResponse>(okResult.Value);
            Assert.Equal("Driver Found", response.Message);
            Assert.Equal(driverDTO, response.Data);
        }

        [Fact]
        public async Task PostDriver_ReturnsOk_WhenDriverDTOIsCorrect()
        {
            // Arrange
            string driverUserName = "correctdriver";


            var driverDTO = new UserDTO(
                driverUserName,
                "Miguel",
                "Gonzalez",
                "miguel@hotmail.com",
                new DateTime(1990, 1, 1),
                "123456789"
             );

            // Act
            var result = await _controller.PostDriver(driverDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<ControllerResponse>(okResult.Value);
            Assert.Equal("Driver Registered", response.Message);
        }
    }
}
