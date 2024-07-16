using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Data.Repositories.Interfaces;

namespace Gauchada.Backend.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _passengerRepository;
        public PassengerService(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        //  Methods
        public async Task<bool> AddPassenger(UserDTO passenger)
        {
            if (passenger.Birth > DateTime.Now)
                return false;

            await _passengerRepository.AddPassenger(new PassengerEntity(passenger));
            if (_passengerRepository.GetPassengerByUserName(passenger.UserName) == null)
                return false;

            return true;
        }

        public async Task<UserDTO?> GetPassengerByUserName(string passengerUserName)
        {
            var passengerEntity = await _passengerRepository.GetPassengerByUserName(passengerUserName);

            if (passengerEntity != null)
                return new UserDTO(passengerEntity.UserName, passengerEntity.Name, passengerEntity.LastName, passengerEntity.Email, passengerEntity.Birth, passengerEntity.PhoneNumber);
            return null;
        }
    }
}
