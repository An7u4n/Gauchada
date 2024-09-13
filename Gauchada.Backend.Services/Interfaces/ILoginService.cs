using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<string> AuthenticateDriver(string username, string password);
        public Task<string> AuthenticatePassenger(string username, string password);
    }
}
