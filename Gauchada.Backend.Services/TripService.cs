using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;

namespace Gauchada.Backend.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<bool> AddPassengerToATrip(int tripId, string passengerUserName)
        {

            try
            {
                await _tripRepository.AddPassengerToATrip(tripId, passengerUserName);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<List<TripDTO>?> GetTripsByLocation(string origin, string destination)
        {
            var trips = await _tripRepository.GetTripsByLocations(origin, destination);

            if (trips == null) 
                return null;
            return trips.Select(t => new TripDTO
            {
                TripId = t.TripId,
                Origin = t.Origin,
                Destination = t.Destination,
                StartDate = t.StartDate,
                DriverUserName = t.DriverUserName,
                CarPlate = t.CarPlate
            }).ToList();
        }

        public Task<List<UserDTO?>> GetUserTrips(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetTrip(TripDTO trip)
        {
            if(trip.Origin == trip.Destination)
                return false;
            if (trip.StartDate > DateTime.Now)
                return false;
            var tripEntity = new TripEntity
            {
                Origin = trip.Origin,
                Destination = trip.Destination,
                StartDate = trip.StartDate,
                DriverUserName = trip.DriverUserName,
                CarPlate = trip.CarPlate
            };
            await _tripRepository.CreateTrip(tripEntity);
            return true;
        }
    }
}
