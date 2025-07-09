using System.ComponentModel.DataAnnotations;

namespace Gauchada.Backend.Model.Entity
{
    public class CarEntity
    {
        [Key] [Required] [MaxLength(7)] public required string CarPlate { get; set; }
        [Required] [MaxLength(35)] public required string Brand { get; set; }
        [Required] [MaxLength(35)] public required string Model { get; set; }
        [Required] [MaxLength(20)] public required string Color { get; set; }
        public int MaxPassengers { get; set; }
        [Required] [MaxLength(32)] public required string OwnerUserName { get; set; }
        public DriverEntity? Owner { get; set; }
        public ICollection<TripEntity> Trips { get; set; } = new List<TripEntity>();
    }
}
