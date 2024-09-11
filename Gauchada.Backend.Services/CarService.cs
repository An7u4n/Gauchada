using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Services.Tools;

namespace Gauchada.Backend.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public Task<bool> DeleteCar(string carPlate)
        {
            throw new NotImplementedException();
        }

        public async Task<CarDTO?> GetCarByPlate(string carPlate)
        {
            try
            {
                var car = await _carRepository.GetCarByPlate(carPlate);
                if (car == null)
                    throw new Exception("Car not found");
                return new CarDTO
                {
                    CarPlate = car.CarPlate,
                    Brand = car.Brand,
                    Model = car.Model,
                    Color = car.Color,
                    OwnerUserName = car.OwnerUserName,
                    MaxPassengers = car.MaxPassengers
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CarDTO>> GetCarsByUserName(string driverUserName)
        {
            try
            {
                var cars = await _carRepository.GetCarsByUserName(driverUserName);
                if (cars == null)
                    throw new Exception("Cars not found");
                return cars.Select(c => new CarDTO
                {
                    CarPlate = c.CarPlate,
                    Brand = c.Brand,
                    Model = c.Model,
                    Color = c.Color,
                    OwnerUserName = c.OwnerUserName,
                    MaxPassengers = c.MaxPassengers
                }).ToList();

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SaveCar(CarDTO car)
        {
            try
            {
                var newCar = new CarEntity
                {
                    CarPlate = StringTools.ToUppercase(car.CarPlate),
                    Brand = StringTools.ToCapitalizedCase(car.Brand),
                    Model = StringTools.ToCapitalizedCase(car.Model),
                    Color = car.Color,
                    OwnerUserName = car.OwnerUserName,
                    MaxPassengers = car.MaxPassengers
                };
                await _carRepository.SaveCar(newCar);
            }
            catch(Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }
    }
}
