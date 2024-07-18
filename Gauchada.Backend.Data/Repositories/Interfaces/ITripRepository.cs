using Gauchada.Backend.Model.Entity;

namespace Gauchada.Backend.Data.Repositories.Interfaces
{
    public interface ITripRepository
    {
        Task CreateTrip(TripEntity trip);
        Task<List<TripEntity>?> GetTripsByLocations(string origin, string destination);
        Task AddPassengerToATrip(int tripId, string passengerUserName);
        Task RemovePassengerFromATrip(int tripId, string passengerUserName);
        Task<TripEntity?> GetTripById(int tripId);
    }
}
