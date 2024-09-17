using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Services.Tools;

namespace Gauchada.Backend.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ICarRepository _carRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IChatRepository _chatRepository;

        public TripService(ITripRepository tripRepository, IDriverRepository driverService, ICarRepository carService, IPassengerRepository passengerRepository, IChatRepository chatRepository)
        {
            _tripRepository = tripRepository;
            _chatRepository = chatRepository;
            _driverRepository = driverService;
            _carRepository = carService;
            _passengerRepository = passengerRepository;
        }

        public async Task<List<TripDTO>> GetTripsByExactDate(string origin, string destination, DateTime date)
        {
            try
            {
                if (date < DateTime.Now)
                    throw new Exception("Date can't be in the past");
                var trips = await _tripRepository.GetTripsByExactDate(origin, destination, date);

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

        public async Task<List<TripDTO>> GetTripsByDateRange(string origin, string destination, DateTime minDate, DateTime maxDate)
        {
            try
            {
                if (minDate < DateTime.Now)
                    throw new Exception("Date can't be in the past");

                if (maxDate < minDate)
                    throw new Exception("Max date cant be earlier than min date");

                var trips = await _tripRepository.GetTripsByDateRange(origin, destination, minDate, maxDate);

                if (trips == null || trips.Count == 0)
                    throw new Exception($"No trips found between {origin} and {destination} with explicited dates");

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

        public async Task<List<TripDTO>> GetUserTrips(string passengerUserName)
        {
            try
            {
                var trips = await _tripRepository.GetTripsByPassenger(passengerUserName);
                if(trips == null || trips.Count == 0)
                    throw new Exception($"No trips found for {passengerUserName}");

                List<TripDTO> tripsDTOs = trips.Select(t => new TripDTO
                {
                    CarPlate = t.CarPlate,
                    DriverUserName = t.DriverUserName,
                    Origin = t.Origin,
                    Destination = t.Destination,
                    StartDate = t.StartDate,
                    TripId = t.TripId
                }).ToList();
                return tripsDTOs;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserDTO>> GetTripPassengers(int tripId)
        {
            try
            {
                var passengers = await _tripRepository.GetTripPassengers(tripId);
                return passengers.Select(p => new UserDTO
                (
                    p.UserName,
                    p.Name,
                    p.LastName,
                    p.Email,
                    p.Birth,
                    p.PhoneNumber,
                    p.PhotoSrc
                )).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddPassengerToATrip(int tripId, string passengerUserName)
        {
            try
            {
                var passenger = await _passengerRepository.GetPassengerByUserName(passengerUserName) ??
                    throw new Exception($"Passenger {passengerUserName} doesn't exists");
                var trip = await _tripRepository.GetTripById(tripId) ??
                    throw new Exception($"Trip {tripId} doesn't exists");
                if (trip.StartDate < DateTime.Now)
                    throw new Exception("Trip has started");
                var car = await _carRepository.GetCarByPlate(trip.CarPlate) ??
                    throw new Exception($"Car doesn't exists");
                if (trip.Passengers.Count == car.MaxPassengers - 1)
                    throw new Exception("Trip is full");
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

        public async Task<TripDTO> SetTrip(TripDTO trip)
        {
            try
            {
                if (trip.Origin == trip.Destination)
                    throw new Exception("Origin and destination can't be the same");
                if (trip.StartDate < DateTime.Now)
                    throw new Exception("StartDate can't be in the past");

                var driver = await _driverRepository.GetDriverByUserName(trip.DriverUserName) ?? 
                    throw new Exception("Driver not found");
                var car = await _carRepository.GetCarByPlate(trip.CarPlate) ?? 
                    throw new Exception("Car not found");
            
                var tripEntity = new TripEntity
                {
                    Origin = StringTools.ToCapitalizedCase(trip.Origin),
                    Destination = StringTools.ToCapitalizedCase(trip.Destination),
                    StartDate = trip.StartDate,
                    DriverUserName = trip.DriverUserName,
                    CarPlate = trip.CarPlate,
                    Driver = driver,
                    Car = car
                };
                var tripId = await _tripRepository.CreateTrip(tripEntity);
                await _chatRepository.CreateTripChat(tripId);
                var createdTripEntity = await _tripRepository.GetTripById(tripId);
                if(createdTripEntity == null)
                    throw new Exception("Trip not created");
                var createdTrip = new TripDTO(createdTripEntity);
                return createdTrip;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
