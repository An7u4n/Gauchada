using Gauchada.Backend.API.Controllers;
using Gauchada.Backend.Data;
using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Model.Response;
using Gauchada.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.ApiTest
{
    public class CarControllerTests : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private AppDbContext _dbContext;
        private Mock<CarRepository>? _mockCarRepository;
        private Mock<CarService>? _carService;
        private CarsController? _controller;

        public CarControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CarTestDatabase0")
                .Options;
            _dbContext = new AppDbContext(_dbContextOptions);
            _mockCarRepository = new Mock<CarRepository>(_dbContext);
            _carService = new Mock<CarService>(_mockCarRepository.Object);
            _controller = new CarsController(_carService.Object);
        }

        [Fact]
        public async Task GetCarsByUserName_ReturnsCars_WhenUserExistsAndHasCars()
        {
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
            var carEntities = new List<CarEntity>
            {
                new CarEntity
                {
                    CarPlate = "ABC123",
                    Brand = "Ford",
                    Model = "Fiesta",
                    Color = "Red",
                    OwnerUserName = driverUserName,
                    MaxPassengers = 5
                },
                new CarEntity
                {
                    CarPlate = "DEF456",
                    Brand = "Chevrolet",
                    Model = "Corsa",
                    Color = "Blue",
                    OwnerUserName = driverUserName,
                    MaxPassengers = 4
                }
            };
            _dbContext.Cars.AddRange(carEntities);
            _dbContext.SaveChanges();
            var carDTOs = carEntities.Select(c => new CarDTO
            {
                CarPlate = c.CarPlate,
                Brand = c.Brand,
                Model = c.Model,
                Color = c.Color,
                OwnerUserName = c.OwnerUserName,
                MaxPassengers = c.MaxPassengers
            }).ToList();
            var result = await _controller.GetCarsByUserName(driverUserName);
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<ControllerResponse>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<ControllerResponse>(okResult.Value);
            Assert.Equal("User Cars Found", response.Message);
            Assert.Equal(carDTOs, response.Data);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _mockCarRepository = null;
            _carService = null;
            _controller = null;
        }
    }
}
