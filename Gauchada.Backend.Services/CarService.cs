using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;

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

        public async Task<CarDTO?> GetCar(string carPlate)
        {
            var car = await _carRepository.GetCar(carPlate);
            if(car == null)
                return null;
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

        public Task<List<CarDTO?>> GetCarsByUserName(string driverUserName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveCar(CarDTO car)
        {
            var newCar = new CarEntity
            {
                CarPlate = car.CarPlate,
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                OwnerUserName = car.OwnerUserName,
                MaxPassengers = car.MaxPassengers
            };

            await _carRepository.SaveCar(newCar);

            var savedCar = await _carRepository.GetCar(car.CarPlate);
            if (savedCar == null)
                return false;
            return true;
        }
    }
}
