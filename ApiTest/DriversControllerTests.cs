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
using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Gauchada.Backend.ApiTest
{
    public class DriversControllerTests : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private AppDbContext? _dbContext;
        private Mock<DriverRepository>? _mockDriverRepository;
        private Mock<IFileStorageService>? _mockFileStorageService;
        private Mock<DriverService>? _mockDriverService;
        private DriversController? _controller;
        public DriversControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DriverTestDatabase")
                .Options;
            _dbContext = new AppDbContext(_dbContextOptions);

            _mockDriverRepository = new Mock<DriverRepository>(_dbContext);
            _mockFileStorageService = new Mock<IFileStorageService>();
            _mockDriverService = new Mock<DriverService>(_mockDriverRepository.Object, _mockFileStorageService.Object);
            _controller = new DriversController(_mockDriverService.Object);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _mockDriverRepository = null;
            _mockFileStorageService = null;
            _mockDriverService = null;
            _controller = null;
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
            Assert.Equal("Driver not found", response.Message);
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
                PhoneNumber = "123456789",
                PhotoSrc = "photo.jpg"
            };
            _dbContext.Drivers.Add(driverEntity);
            _dbContext.SaveChanges();

            var driverDTO = new UserDTO(
                 driverEntity.UserName,
                 driverEntity.Name,
                 driverEntity.LastName,
                 driverEntity.Email,
                 driverEntity.Birth,
                 driverEntity.PhoneNumber,
                 driverEntity.PhotoSrc
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
            var driverDTO = new AddUserDTO(
                driverUserName,
                "Miguel",
                "Gonzalez",
                "miguel@hotmail.com",
                new DateTime(1990, 1, 1),
                "123456789",
                CreateFakeImage()
             );

            _mockFileStorageService.Setup(s => s.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string[]>(), It.IsAny<string>()))
            .ReturnsAsync("testImage.jpg");

            // Act
            var result = await _controller.PostDriver(driverDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<ControllerResponse>(okResult.Value);
            Assert.Equal("Driver Registered", response.Message);
        }

        // Image Faking method
        private IFormFile CreateFakeImage()
        {
            var fileContent = "This is a test image";
            var fileName = "testImage.png";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            var formFile = new FormFile(stream, 0, stream.Length, "testImage", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpg"
            };
            return formFile;
        }
    }
}
