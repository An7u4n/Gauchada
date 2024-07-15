using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;

namespace Gauchada.Backend.Data.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly AppDbContext _context;
        public PassengerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPassenger(PassengerEntity passenger)
        {
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();
        }

        public async Task<PassengerEntity> GetPassengerByUserName(string userName)
        {
            return await _context.Passengers.FindAsync(userName);
        }
    }
}
