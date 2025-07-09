using System.ComponentModel.DataAnnotations;

namespace Gauchada.Backend.Model.Entity
{
    public class TripEntity
    {
        [Key] public int TripId { get; set; }
        public DateTime StartDate { get; set; }
        [Required] [MaxLength(80)] public required string Origin { get; set; }
        [Required] [MaxLength(80)] public required string Destination { get; set; }
        [Required] [MaxLength(32)] public required string DriverUserName { get; set; }
        [Required] [MaxLength(7)] public required string CarPlate { get; set; }
        public virtual DriverEntity? Driver { get; set; }
        public virtual CarEntity? Car { get; set; }
        public virtual Chat? Chat { get; set; }
        public virtual ICollection<PassengerEntity> Passengers { get; set; } = new List<PassengerEntity>();
    }
}
