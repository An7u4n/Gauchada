using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;

namespace Gauchada.Backend.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;
        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CarEntity?> GetCar(string carPlate)
        {
            return await _context.Cars.FindAsync(carPlate);
        }

        public async Task SaveCar(CarEntity car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }
    }
}
