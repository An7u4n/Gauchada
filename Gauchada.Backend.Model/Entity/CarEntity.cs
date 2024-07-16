using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Model.Entity
{
    public class CarEntity
    {
        [Key] [Required] [MaxLength(7)] public string CarPlate { get; set; }
        [Required] [MaxLength(35)] public string Brand { get; set; }
        [Required] [MaxLength(35)] public string Model { get; set; }
        [Required] [MaxLength(20)] public string Color { get; set; }
        [Required] public int MaxPassengers { get; set; }
        [Required] [MaxLength(32)] public string OwnerUserName { get; set; }
        public DriverEntity Owner { get; set; }
    }
}
