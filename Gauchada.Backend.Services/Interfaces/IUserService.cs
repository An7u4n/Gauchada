using Gauchada.Backend.Model.DTO;
namespace Gauchada.Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<PassengerDTO> GetPassengerByUserName(string passengerUserName); 
        Task<bool> RegisterPassenger(PassengerDTO passenger);
    }
}
