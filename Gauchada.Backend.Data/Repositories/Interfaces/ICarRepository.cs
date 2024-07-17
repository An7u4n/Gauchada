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
        Task<CarEntity?> GetCar(string carPlate);
    }
}
