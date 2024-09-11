using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Data.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext _context;
        public DriverRepository(AppDbContext context)
        {
            _context = context;
        }

        //  Methods
        public async Task AddDriver(DriverEntity driver)
        {
            try
            {
                if(_context.Drivers.Find(driver.UserName) != null)
                    throw new Exception("Driver Already Exists");
                _context.Drivers.Add(driver);
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

        public async Task<DriverEntity?> GetDriverByUserName(string driverUserName)
        {
            try
            {
                return await _context.Drivers.FindAsync(driverUserName);
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

        public async Task<List<TripEntity>?> GetDriverTrips(string driverUserName)
        {
            try
            {
                var driver = await _context.Drivers.Include(d => d.Trips).FirstOrDefaultAsync(d => d.UserName == driverUserName);
                return driver?.Trips.ToList();
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
