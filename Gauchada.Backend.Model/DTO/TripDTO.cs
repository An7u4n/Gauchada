using Gauchada.Backend.Model.Entity;

namespace Gauchada.Backend.Model.DTO
{
    public class TripDTO
    {
        public int? TripId { get; set; }
        public required string Origin { get; set; }
        public required string Destination { get; set; }
        public required DateTime StartDate { get; set; }
        public required string DriverUserName { get; set; }
        public required string CarPlate { get; set; }
        public TripDTO() { }
        public TripDTO(TripEntity trip)
        {
            TripId = trip.TripId;
            Origin = trip.Origin;
            Destination = trip.Destination;
            StartDate = trip.StartDate;
            DriverUserName = trip.DriverUserName;
            CarPlate = trip.CarPlate;
        }
    }
    
    public class TripGetDTO
    {
        public int TripId { get; set; }
        public required string Origin { get; set; }
        public required string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public required string CarPlate { get; set; }
        public DriverEntity? Driver { get; set; }
        public required int FreeSeats { get; set; }
    }
}
