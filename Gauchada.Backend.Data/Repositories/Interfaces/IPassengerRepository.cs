using Gauchada.Backend.Model.Entity;
namespace Gauchada.Backend.Data.Repositories.Interfaces
{
    public interface IPassengerRepository
    {
        Task AddPassenger(PassengerEntity passenger);
        Task<PassengerEntity?> GetPassengerByUserName(string userName);
    }
}
