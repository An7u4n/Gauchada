using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;
using System.Linq.Expressions;

namespace Gauchada.Backend.Services
{
    public class DriverService : IDriverService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IDriverRepository _driverRepository;
        public DriverService(IDriverRepository driverRepository, IFileStorageService fileStorageService)
        {
            _driverRepository = driverRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task AddDriver(AddUserDTO driver)
        {
            try
            {
                if (driver.Birth > DateTime.Now.AddYears(-18))
                    throw new Exception("Driver must be adult");
                if (driver.Photo.Length > 1 * 1024 * 1024)
                {
                    throw new Exception("Image size should not exceed 1 MB");
                }

                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await _fileStorageService.SaveFileAsync(driver.Photo, allowedFileExtentions, "driver");

                await _driverRepository.AddDriver(new DriverEntity(driver, createdImageName));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);   
            }
        }

        public async Task<UserDTO?> GetDriverByUserName(string userName)
        {
            try
            {
                var driverEntity = await _driverRepository.GetDriverByUserName(userName);
                if (driverEntity == null)
                    throw new Exception("Driver not found");
                return new UserDTO(driverEntity.UserName, driverEntity.Name, driverEntity.LastName, driverEntity.Email, driverEntity.Birth, driverEntity.PhoneNumber, driverEntity.PhotoSrc);
            }
            catch(Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }
    }
}
