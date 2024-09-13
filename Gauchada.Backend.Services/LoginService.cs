using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Services.Tools;

namespace Gauchada.Backend.Services
{
    public class LoginService : ILoginService
    {
        private IDriverRepository _driverRepository;
        private IPassengerRepository _passengerRepository;
        public LoginService(IDriverRepository driverRepository, IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
            _driverRepository = driverRepository;
        }

        public async Task<string> AuthenticateDriver(string username, string password)
        {
            var driver = await _driverRepository.GetDriverByUserName(username);
            // Third Party User Authentication Service To Be Implemented
            if (driver != null && password == "123456")
            {
                return JWTHandler.GenerateJwtToken(driver);
            }
            else
            {
                throw new Exception("Invalid username or password");
            }
        }

        public async Task<string> AuthenticatePassenger(string username, string password)
        {
            var passenger = await _passengerRepository.GetPassengerByUserName(username);

            if (passenger != null && password == "123456")
            {
                return JWTHandler.GenerateJwtToken(passenger);
            }
            else
            {
                throw new Exception("Invalid username or password");
            }
        }
    }
}
