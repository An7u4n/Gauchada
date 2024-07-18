using Gauchada.Backend.API.Controllers;
using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Data;
using Gauchada.Backend.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using Gauchada.Backend.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Model.DTO;

public class TripsControllerTests : IDisposable
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;
    private AppDbContext? _dbContext;
    private Mock<TripRepository>? _mockTripRepository;
    private Mock<CarRepository>? _mockCarRepository;
    private Mock<DriverRepository>? _mockDriverRepository;
    private Mock<TripService>? _tripService;
    private Mock<PassengerRepository>? _mockPassengerRepository;
    private TripsController? _controller;

    public TripsControllerTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        _dbContext = new AppDbContext(_dbContextOptions);
        _mockTripRepository = new Mock<TripRepository>(_dbContext);
        _mockCarRepository = new Mock<CarRepository>(_dbContext);
        _mockDriverRepository = new Mock<DriverRepository>(_dbContext);
        _mockPassengerRepository = new Mock<PassengerRepository>(_dbContext);
        _tripService = new Mock<TripService>(_mockTripRepository.Object, _mockDriverRepository.Object, _mockCarRepository.Object, _mockPassengerRepository.Object);
        _controller = new TripsController(_tripService.Object);
    }   

    public void Dispose()
    {
        _dbContext.Dispose();
        _mockTripRepository = null;
        _mockCarRepository = null;
        _mockDriverRepository = null;
        _mockPassengerRepository = null;
        _tripService = null;
        _controller = null;
    }

    [Fact]
    public async Task GetTripsByLocations_ReturnsNotFound_WhenThereIsNoTripsForThatTwoCities()
    {
        // Arrange
        string origin = "originwithouttrips";
        string destination = "destinationwithouttrips";

        // Act
        var result = await _controller.GetTripsByLocations(origin, destination);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(notFoundResult.Value);
        Assert.Equal($"No trips found between {origin} and {destination}", response.Message);
    }

    [Fact]
    public async Task GetTripsByLocations_ReturnsOkAndTrips_WhenThereIsTripsForThatTwoCities()
    {
        // Arrange
        string origin = "originwithtrips";
        string destination = "destinationwithtrips";

        _dbContext.Trips.Add(new TripEntity()
        {
            CarPlate = "carplate",
            Destination = destination,
            DriverUserName = "driverusername",
            Origin = origin,
            StartDate = DateTime.Now
        });
        _dbContext.SaveChanges();

        // Act
        var result = await _controller.GetTripsByLocations(origin, destination);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(okObjectResult.Value);
        Assert.Equal("Trips Found", response.Message);
    }

    [Fact]
    public async Task AddPassengerToATrip_ReturnsBadRequest_WhenPassengerNotExists()
    {
        // Arrange
        int tripId = 1;
        string passengerUserName = "passengerwhoisnotsaved";

        _dbContext.Trips.Add(new TripEntity()
        {
            CarPlate = "carplate",
            Destination = "destination",
            DriverUserName = "driverusername",
            Origin = "origin",
            StartDate = DateTime.Now,
            TripId = tripId
        });
        _dbContext.SaveChanges();

        // Act
        var result = await _controller.AddPassengerToATrip(tripId, passengerUserName);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(badRequestObjectResult.Value);
        Assert.Equal($"Passenger {passengerUserName} doesn't exists", response.Message);
    }

    [Fact]
    public async Task AddPassengerToATrip_ReturnsBadRequest_WhenTripNotExists()
    {
        // Arrange
        int tripId = 1;
        string passengerUserName = "addedpassenger";

        _dbContext.Passengers.Add(new PassengerEntity()
        {
            UserName = passengerUserName,
            Name = "passengername",
            LastName = "passengerlastname",
            Email = "passengeremail@hot.com",
            Birth = DateTime.Now,
            PhoneNumber = "+54938961681"
        });
        _dbContext.SaveChanges();

        // Act
        var result = await _controller.AddPassengerToATrip(tripId, passengerUserName);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(badRequestObjectResult.Value);
        Assert.Equal($"Trip {tripId} doesn't exists", response.Message);
    }

    [Fact]
    public async Task AddPassengerToATrip_ReturnOk_WhenTripIsAdded()
    {
        // Arrange
        int tripId = 1;
        string passengerUserName = "addedpassenger";

        _dbContext.Trips.Add(new TripEntity()
        {
            CarPlate = "carplate",
            Destination = "destination",
            DriverUserName = "driverusername",
            Origin = "origin",
            StartDate = DateTime.Now,
            TripId = tripId
        });
        _dbContext.Passengers.Add(new PassengerEntity()
        {
            UserName = passengerUserName,
            Name = "passengername",
            LastName = "passengerlastname",
            Email = "passengeremail@hot.com",
            Birth = DateTime.Now,
            PhoneNumber = "+54938961681"
        });
        _dbContext.SaveChanges();

        // Act
        var result = await _controller.AddPassengerToATrip(tripId, passengerUserName);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(okObjectResult.Value);
        Assert.Equal("Passenger Added To The Trip", response.Message);
    }

    [Fact]
    public async Task PostTrip_ReturnsBadRequest_WhenDriverDoesntExists()
    {
        // Arrange
        int tripId = 1;
        string driverUserName = "notexstingdriver";
        var notValidTrip = new TripDTO
        {
            CarPlate = "carplate",
            Destination = "destination",
            DriverUserName = driverUserName,
            Origin = "origin",
            StartDate = DateTime.Now,
            TripId = tripId
        };

        // Act
        var result = await _controller.PostTrip(notValidTrip);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(badRequestObjectResult.Value);
        Assert.Equal("Driver not found", response.Message);
    }

    [Fact]
    public async Task PostTrip_ReturnsBadRequest_WhenOriginAndDestinationsAreTheSame()
    {
        // Arrange
        int tripId = 1;
        string driverUserName = "exitingDriver";
        _dbContext.Drivers.Add(new DriverEntity()
        {
            Name = "asd",
            LastName = "asd",
            Email = "asd@asd.com",
            Birth = DateTime.Now,
            PhoneNumber = "+54938961681",
            UserName = driverUserName
        });
        _dbContext.SaveChanges();

        var notValidTrip = new TripDTO
        {
            CarPlate = "carplate",
            Destination = "samecity",
            DriverUserName = "driverusername",
            Origin = "samecity",
            StartDate = DateTime.Now,
            TripId = tripId
        };

        // Act
        var result = await _controller.PostTrip(notValidTrip);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        var response = Assert.IsType<ControllerResponse>(badRequestObjectResult.Value);
        Assert.Equal("Origin and destination can't be the same", response.Message);
    }
}
