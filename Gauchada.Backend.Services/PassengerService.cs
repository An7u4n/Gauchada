using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Data.Repositories;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Data.Repositories.Interfaces;

namespace Gauchada.Backend.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IPassengerRepository _passengerRepository;
        public PassengerService(IPassengerRepository passengerRepository, IFileStorageService fileStorageService)
        {
            _passengerRepository = passengerRepository;
            _fileStorageService = fileStorageService;
        }

        //  Methods
        public async Task AddPassenger(AddUserDTO passenger)
        {
            try
            {
                if (passenger.Birth > DateTime.Now.AddYears(-16))
                    throw new Exception("Passenger must be 16 years old or older");
                if (passenger.Photo.Length > 1 * 1024 * 1024)
                {
                    throw new Exception("Image size should not exceed 1 MB");
                }

                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await _fileStorageService.SaveFileAsync(passenger.Photo, allowedFileExtentions, "passenger");

                await _passengerRepository.AddPassenger(new PassengerEntity(passenger, createdImageName));
            }
            catch(Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }

        public async Task<UserDTO> GetPassengerByUserName(string passengerUserName)
        {
            try
            {
                var passengerEntity = await _passengerRepository.GetPassengerByUserName(passengerUserName);
                if (passengerEntity == null)
                    throw new Exception("Passenger not found");

                return new UserDTO(passengerEntity.UserName, passengerEntity.Name, passengerEntity.LastName, passengerEntity.Email, passengerEntity.Birth, passengerEntity.PhoneNumber, passengerEntity.PhotoSrc);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
