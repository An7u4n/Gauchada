using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;
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

        public async Task AddPassengerToATrip(int tripId, string passengerUserName)
        {
            var passenger = _context.Passengers.FirstOrDefault(p => p.UserName == passengerUserName);
            var trip = _context.Trips.Find(tripId);

            if (passenger == null || trip == null)
            {
                throw new InvalidOperationException("Passenger or Trip not found.");
            }

            passenger.Trips.Add(trip);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTrip(TripEntity trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TripEntity>?> GetTripsByLocations(string origin, string destination)
        {
            return await _context.Trips.Where(t => t.Origin == origin && t.Destination == destination).ToListAsync();
        }
    }
}
