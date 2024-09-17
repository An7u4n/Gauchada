using Gauchada.Backend.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Model.DTO
{
    public class TripDTO
    {
        public int? TripId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public string DriverUserName { get; set; }
        public string CarPlate { get; set; }
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
    public class TripGetDTO {
        public int TripId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public string CarPlate { get; set; }
        public DriverEntity Driver { get; set; }
        public int FreeSeats { get; set; }
    }
}
