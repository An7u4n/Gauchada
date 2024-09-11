using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gauchada.Backend.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext _context;
        public TripRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateTrip(TripEntity trip)
        {
            try
            {
                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();
            }
            catch(SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            } 
            catch(Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }

        public async Task<List<TripEntity>?> GetTripsByDateRange(string origin, string destination, DateTime minDate, DateTime maxDate)
        {
            try
            {
                return await _context.Trips.Where(t => t.Origin == origin && t.Destination == destination && t.StartDate >= minDate && t.StartDate <= maxDate).ToListAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }

        public async Task AddPassengerToATrip(int tripId, string passengerUserName)
        {
            try
            {
                var passenger = await _context.Passengers.Include(p => p.Trips).FirstOrDefaultAsync(p => p.UserName == passengerUserName);
                if (passenger == null)
                {
                    throw new InvalidOperationException("Passenger not found.");
                }
                if(passenger.Trips.Any(t => t.TripId == tripId))
                {
                    throw new Exception("This passenger is already in this trip.");
                }

                var trip = await _context.Trips.FindAsync(tripId);
                if (trip == null)
                {
                    throw new InvalidOperationException("Trip not found.");
                }

                passenger.Trips.Add(trip);
                await _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }

        public Task RemovePassengerFromATrip(int tripId, string passengerUserName)
        {
            try
            {
                var passenger = _context.Passengers.FirstOrDefault(p => p.UserName == passengerUserName);
                var trip = _context.Trips.Find(tripId);

                if (passenger == null || trip == null)
                {
                    throw new InvalidOperationException("Passenger or Trip not found.");
                }

                passenger.Trips.Remove(trip);
                return _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }

        public async Task<TripEntity?> GetTripById(int tripId)
        {
            try
            {
                var trip = await _context.Trips.Include(t => t.Passengers).FirstOrDefaultAsync(t => t.TripId == tripId);
                return trip;
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }

        public async Task<List<TripEntity>?> GetTripsByPassenger(string passengerUserName)
        {
            try
            {
                var passenger = await _context.Passengers.Include(p => p.Trips).FirstOrDefaultAsync(p => p.UserName == passengerUserName) ??
                        throw new Exception($"Passenger {passengerUserName} doesn't exists");
                return passenger.Trips.ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }

        public async Task<List<PassengerEntity>> GetTripPassengers(int tripId)
        {
            try
            {
                var trip = await _context.Trips.Include(t => t.Passengers).FirstOrDefaultAsync(t => t.TripId == tripId);
                if (trip == null)
                    throw new Exception("Trip doesnt found");
                return trip.Passengers.ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }

        public async Task<List<TripEntity>?> GetTripsByExactDate(string origin, string destination, DateTime date)
        {
            try
            {
                return await _context.Trips.Include(t => t.Car).Include(t => t.Driver).Where(t => t.Origin == origin && t.Destination == destination && t.StartDate.Date == date).ToListAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception("DB Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknow Error In Repository: " + ex.Message);
            }
        }
    }
}
