using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;
using Microsoft.Data.SqlClient;

namespace Gauchada.Backend.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;
        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CarEntity?> GetCarByPlate(string carPlate)
        {
            try
            {
                return await _context.Cars.FindAsync(carPlate);
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

        public async Task SaveCar(CarEntity car)
        {
            try
            {
                _context.Cars.Add(car);
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
    }
}
