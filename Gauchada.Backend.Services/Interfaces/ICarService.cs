using Gauchada.Backend.Model.DTO;

namespace Gauchada.Backend.Services.Interfaces
{
    public interface ICarService
    {
        Task<CarDTO?> GetCarByPlate(string carPlate);
        Task<List<CarDTO>> GetCarsByUserName(string driverUserName);
        Task SaveCar(CarDTO car);
        Task DeleteCar(string carPlate);
    }
}
