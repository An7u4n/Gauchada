using Gauchada.Backend.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Data.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task SaveCar(CarEntity car);
        Task<CarEntity?> GetCarByPlate(string carPlate);
        Task<List<CarEntity>?> GetCarsByUserName(string userName);
        Task DeleteCar(CarEntity car);
    }
}
