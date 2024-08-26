using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Model.DTO
{
    public class CarDTO
    {
        public string CarPlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string OwnerUserName { get; set; }
        public int MaxPassengers { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as CarDTO;

            if (other == null)
                return false;

            return this.CarPlate == other.CarPlate &&
                   this.Brand == other.Brand &&
                   this.Model == other.Model &&
                   this.Color == other.Color &&
                   this.OwnerUserName == other.OwnerUserName &&
                   this.MaxPassengers == other.MaxPassengers;
        }
    }
}
