using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity.Abstract;

namespace Gauchada.Backend.Model.Entity
{
    public class PassengerEntity : UserAbstract
    {

        public ICollection<TripEntity> Trips;

        public PassengerEntity() { }
        public PassengerEntity(PassengerDTO passenger)
        : base(passenger.UserName, passenger.Name, passenger.LastName, passenger.Email, passenger.Birth, passenger.PhoneNumber)
        {
        }
    }
}
