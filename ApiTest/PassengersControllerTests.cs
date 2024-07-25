using System.Text;
using System.Threading.Tasks;
using Gauchada.Backend.API.Controllers;
using Gauchada.Backend.Data;
using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Model.Response;
using Gauchada.Backend.Services;
using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class PassengerControllerTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;
    private readonly AppDbContext _dbContext;
    private readonly Mock<PassengerRepository> _mockPassengerRepository;
    private readonly Mock<IFileStorageService> _mockFileStorageService;
    private readonly Mock<PassengerService> _mockUserService;
    private readonly PassengersController _controller;

    public PassengerControllerTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new AppDbContext(_dbContextOptions);
        _mockPassengerRepository = new Mock<PassengerRepository>(_dbContext);
        _mockFileStorageService = new Mock<IFileStorageService>();
        _mockUserService = new Mock<PassengerService>(_mockPassengerRepository.Object, _mockFileStorageService.Object);
        _controller = new PassengersController(_mockUserService.Object);
    }

    [Fact]
    public async Task GetPassengerByUserName_ReturnsNotFound_WhenPassengerDoesNotExist()
    {
        // Arrange
        string passengerUserName = "nonexistentpassenger";

        // Act
        var result = await _controller.GetPassengerByUserName(passengerUserName);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(notFoundResult.Value);
        Assert.Equal("Passenger not found", response.Message);
    }

    [Fact]
    public async Task GetPassengerByUserName_ReturnsOk_WhenPassengerExists()
    {
        // Arrange
        string passengerUserName = "existinguser";
        var passengerEntity = new PassengerEntity
        {
            UserName = passengerUserName,
            Name = "Miguel",
            LastName = "Centurion",
            Email = "miguel@hotmail.com",
            Birth = new DateTime(1990, 1, 1),
            PhoneNumber = "1234567890",
            PhotoSrc = "photo.jpg"
        };

        _dbContext.Passengers.Add(passengerEntity);
        _dbContext.SaveChanges();

        var passengerDTO = new UserDTO(
            passengerEntity.UserName,
            passengerEntity.Name,
            passengerEntity.LastName,
            passengerEntity.Email,
            passengerEntity.Birth,
            passengerEntity.PhoneNumber,
            passengerEntity.PhotoSrc
        );

        // Act
        var result = await _controller.GetPassengerByUserName(passengerUserName);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(okResult.Value);
        Assert.Equal(passengerDTO, response.Data);
        Assert.Equal("Passenger Found", response.Message);
    }

    [Fact]
    public async Task PostPassenger_ReturnOk_WhenDataIsCorrect()
    {
        // Arrange
        var passengerDTO = new AddUserDTO(
            "CorrectPassenger",
            "Miguel",
            "Centurion",
            "miguel@hotmail.com",
            new DateTime(1990, 1, 1),
            "+54937485147",
            CreateFakeImage()
        );

        _mockFileStorageService.Setup(s => s.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string[]>(), It.IsAny<string>()))
        .ReturnsAsync("testImage.jpg");

        // Act
        var result = await _controller.PostPassenger(passengerDTO);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(okResult.Value);
        Assert.Equal("Passenger Registered", response.Message);
    }

    [Fact]
    public async Task PostPassenger_ReturnBadRequest_WhenBirthIsFuture()
    {
        // Arrange
        var passengerDTO = new AddUserDTO(
            "IncorrectUser",
            "Miguel",
            "Centurion",
            "migue@nothotmail.com",
            new DateTime(2200, 1, 1),
            "+5493485514064",
            CreateFakeImage()
        );

        _mockFileStorageService.Setup(s => s.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string[]>(), It.IsAny<string>()))
            .ReturnsAsync("testImage.jpg");

        // Act
        var result = await _controller.PostPassenger(passengerDTO);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var badResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(badResult.Value);
        Assert.Equal("Passenger must be 16 years old or older", response.Message);
    }

    // Image Faking Method
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
