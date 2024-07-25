using Gauchada.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Services.Interfaces
{
    public interface IDriverService
    {
        public Task<UserDTO?> GetDriverByUserName(string userName);
        public Task AddDriver(AddUserDTO driver);
    }
}
