using Gauchada.Backend.Model.Entity;
namespace Gauchada.Backend.Data.Repositories.Interfaces
{
    internal interface IPassengerRepository
    {
        void AddPassenger(PassengerEntity passenger);
        PassengerEntity GetPassengerById(int id);
    }
}
