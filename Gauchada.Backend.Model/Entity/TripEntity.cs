using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Model.Entity
{
    public class TripEntity
    {
        [Key] [Required] public int TripId { get; set; }
        [Required] public DateTime StartDate { get; set; }
        [Required] [MaxLength(80)] public string Origin { get; set; }
        [Required] [MaxLength(80)] public string Destination { get; set; }
        [Required] [MaxLength(32)] public string DriverUserName { get; set; }
        [Required] [MaxLength(7)] public string CarPlate { get; set; }
        public DriverEntity Driver { get; set; }
        public CarEntity Car { get; set; }
        public Chat Chat { get; set; }
        public ICollection<PassengerEntity> Passengers { get; set; } = new List<PassengerEntity>();
    }
}
