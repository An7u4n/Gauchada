using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DriverEntity Driver { get; set; }
        public ICollection<PassengerEntity> Passengers { get; set; }
    }
}
