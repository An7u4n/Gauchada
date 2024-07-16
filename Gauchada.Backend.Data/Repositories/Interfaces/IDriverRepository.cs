using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
namespace Gauchada.Backend.Data.Repositories.Interfaces
{
    public interface IDriverRepository
    {
        Task AddDriver(DriverEntity driver);
        Task<DriverEntity?> GetDriverByUserName(string driverUserName);
    }
}