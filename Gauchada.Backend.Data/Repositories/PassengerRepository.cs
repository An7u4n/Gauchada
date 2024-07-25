using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;
using Microsoft.Data.SqlClient;

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
            try
            {
                _context.Passengers.Add(passenger);
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

        public async Task<PassengerEntity?> GetPassengerByUserName(string userName)
        {
            try
            {
                return await _context.Passengers.FindAsync(userName);
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
