using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<bool> AddDriver(UserDTO driver)
        {
            if (driver.Birth > DateTime.Now)
                return false;

            await _driverRepository.AddDriver(new DriverEntity(driver));
            if (_driverRepository.GetDriverByUserName(driver.UserName) == null)
                return false;

            return true;
        }

        public async Task<UserDTO?> GetDriverByUserName(string userName)
        {
            var driverEntity = await _driverRepository.GetDriverByUserName(userName);

            if (driverEntity != null)
                return new UserDTO(driverEntity.UserName, driverEntity.Name, driverEntity.LastName, driverEntity.Email, driverEntity.Birth, driverEntity.PhoneNumber);
            return null;
        }
    }
}
