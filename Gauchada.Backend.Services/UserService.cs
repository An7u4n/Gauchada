using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Model.Entity;

namespace Gauchada.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly PassengerRepository _passengerRepository;
        public UserService(PassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task<PassengerDTO> GetPassengerByUserName(string passengerUserName)
        {
            var passengerEntity = await _passengerRepository.GetPassengerByUserName(passengerUserName);

            if (passengerEntity != null)
                return new PassengerDTO(passengerEntity.UserName, passengerEntity.Name, passengerEntity.LastName, passengerEntity.Email, passengerEntity.Birth, passengerEntity.PhoneNumber);
            return null;
        }

        public async Task<bool> RegisterPassenger(PassengerDTO passenger)
        {
            await _passengerRepository.AddPassenger(new PassengerEntity(passenger));

            var newPassenger = _passengerRepository.GetPassengerByUserName(passenger.UserName);
            if (newPassenger == null)
                return false;
            return true;
        }
    }
}
