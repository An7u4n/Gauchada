using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Data.Repositories
{
    internal class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext _context;
        public DriverRepository(AppDbContext context)
        {
            _context = context;
        }

        //  Methods
        public async Task AddDriver(DriverEntity driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
        }

        public async Task<DriverEntity?> GetDriverByUserName(string driverUserName)
        {
            return await _context.Drivers.FindAsync(driverUserName);
        }
    }
}
