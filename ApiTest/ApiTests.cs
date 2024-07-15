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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class PassengerControllerTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;
    private readonly AppDbContext _dbContext;
    private readonly Mock<PassengerRepository> _mockPassengerRepository;
    private readonly Mock<UserService> _userService;
    private readonly PassengersController _controller;

    public PassengerControllerTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new AppDbContext(_dbContextOptions);
        _mockPassengerRepository = new Mock<PassengerRepository>(_dbContext);
        _userService = new Mock<UserService>(_mockPassengerRepository.Object);
        _controller = new PassengersController(_userService.Object);
    }

    [Fact]
    public async Task GetPassengerInfo_ReturnsNotFound_WhenPassengerDoesNotExist()
    {
        // Arrange
        string passengerUserName = "nonexistentuser";

        // Act
        var result = await _controller.GetPassengerInfo(passengerUserName);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(notFoundResult.Value);
        Assert.Equal("Passager Not Found", response.Message);
    }

    [Fact]
    public async Task GetPassengerInfo_ReturnsOk_WhenPassengerExists()
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
            PhoneNumber = "1234567890"
        };

        _dbContext.Passengers.Add(passengerEntity);
        _dbContext.SaveChanges();

        var passengerDTO = new PassengerDTO(
            passengerEntity.UserName,
            passengerEntity.Name,
            passengerEntity.LastName,
            passengerEntity.Email,
            passengerEntity.Birth,
            passengerEntity.PhoneNumber
        );

        // Act
        var result = await _controller.GetPassengerInfo(passengerUserName);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(okResult.Value);
        Assert.Equal(passengerDTO, response.Data);
        Assert.Equal("", response.Message);
    }
}
