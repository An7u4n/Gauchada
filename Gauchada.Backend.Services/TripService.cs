using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;

namespace Gauchada.Backend.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ICarRepository _carRepository;
        private readonly IPassengerRepository _passengerRepository;

        public TripService(ITripRepository tripRepository, IDriverRepository driverService, ICarRepository carService, IPassengerRepository passengerRepository)
        {
            _tripRepository = tripRepository;
            _driverRepository = driverService;
            _carRepository = carService;
            _passengerRepository = passengerRepository;
        }

        public async Task<List<TripDTO>> GetTripsByLocation(string origin, string destination)
        {
            try
            {
                var trips = await _tripRepository.GetTripsByLocations(origin, destination);

                if (trips == null || trips.Count == 0)
                    throw new Exception($"No trips found between {origin} and {destination}");

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<List<UserDTO?>> GetUserTrips(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddPassengerToATrip(int tripId, string passengerUserName)
        {
            try
            {
                var passenger = await _passengerRepository.GetPassengerByUserName(passengerUserName) ??
                    throw new Exception($"Passenger {passengerUserName} doesn't exists");
                var trip = await _tripRepository.GetTripById(tripId) ??
                    throw new Exception($"Trip {tripId} doesn't exists");
                await _tripRepository.AddPassengerToATrip(tripId, passengerUserName);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemovePassengerFromATrip(int tripId, string passengerUserName)
        {
            try
            {
                await _tripRepository.RemovePassengerFromATrip(tripId, passengerUserName);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SetTrip(TripDTO trip)
        {
            try
            {
                if (trip.Origin == trip.Destination)
                    throw new Exception("Origin and destination can't be the same");
                if (trip.StartDate > DateTime.Now)
                    throw new Exception("StartDate can't be in the past");

                var driver = await _driverRepository.GetDriverByUserName(trip.DriverUserName) ?? 
                    throw new Exception("Driver not found");
                var car = await _carRepository.GetCarByPlate(trip.CarPlate) ?? 
                    throw new Exception("Car not found");
            
                var tripEntity = new TripEntity
                {
                    Origin = trip.Origin,
                    Destination = trip.Destination,
                    StartDate = trip.StartDate,
                    DriverUserName = trip.DriverUserName,
                    CarPlate = trip.CarPlate,
                    Driver = driver,
                    Car = car
                };
                await _tripRepository.CreateTrip(tripEntity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
