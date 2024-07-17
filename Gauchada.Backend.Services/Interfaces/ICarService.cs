using Gauchada.Backend.Model.DTO;

namespace Gauchada.Backend.Services.Interfaces
{
    public interface ICarService
    {
        Task<CarDTO?> GetCar(string carPlate);
        Task<List<CarDTO?>> GetCarsByUserName(string driverUserName);
        Task<bool> SaveCar(CarDTO car);
        Task<bool> DeleteCar(string carPlate);
    }
}
