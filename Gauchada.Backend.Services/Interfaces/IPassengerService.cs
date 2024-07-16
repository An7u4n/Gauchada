using Gauchada.Backend.Model.DTO;
namespace Gauchada.Backend.Services.Interfaces
{
    public interface IPassengerService
    {
        Task<bool> AddPassenger(UserDTO passenger);
        Task<UserDTO?> GetPassengerByUserName(string passengerUserName); 
    }
}
