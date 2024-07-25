using Gauchada.Backend.Model.DTO;
namespace Gauchada.Backend.Services.Interfaces
{
    public interface IPassengerService
    {
        Task AddPassenger(AddUserDTO passenger);
        Task<UserDTO> GetPassengerByUserName(string passengerUserName); 
    }
}
