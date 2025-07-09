using Gauchada.Backend.Data.Repositories.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;

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

                var newDriver = new DriverEntity
                {
                    UserName = driver.UserName,
                    Name = driver.Name,
                    LastName = driver.LastName,
                    Email = driver.Email,
                    PhoneNumber = driver.PhoneNumber,
                    Birth = driver.Birth
                };

                if (driver.Photo != null)
                {
                    string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];

                    if (driver.Photo.Length > 1 * 1024 * 1024)
                    {
                        throw new Exception("Image size should not exceed 1 MB");
                    }


                    string createdImageName = await _fileStorageService.SaveFileAsync(driver.Photo, allowedFileExtentions, "driver");


                }


                await _driverRepository.AddDriver(newDriver);
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

                if (driverEntity == null) throw new Exception("Driver not found");

                var driverDto = new UserDTO
                {
                    Birth = driverEntity.Birth,
                    Email = driverEntity.Email,
                    LastName = driverEntity.LastName,
                    Name = driverEntity.Name,
                    PhoneNumber = driverEntity.PhoneNumber,
                    PhotoSrc = driverEntity.PhotoSrc ?? "",
                    UserName = driverEntity.UserName
                };

                return driverDto;
            }
            catch(Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }

        public async Task<List<TripDTO>?> GetDriverTrips(string userName)
        {
            try
            {
                var driverTrips = await _driverRepository.GetDriverTrips(userName);
                if(driverTrips ==  null)
                    throw new Exception("Driver has no trips");
                return driverTrips.Aggregate(new List<TripDTO>(), (acc, trip) =>
                {
                    var tripDto = new TripDTO
                    {
                        CarPlate = trip.CarPlate,
                        Destination = trip.Destination,
                        DriverUserName = trip.DriverUserName,
                        Origin = trip.Origin,
                        StartDate = trip.StartDate,
                        TripId = trip.TripId
                    };

                    acc.Add(tripDto);

                    return acc;
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
