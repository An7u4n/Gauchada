using Gauchada.Backend.Model.DTO;

namespace Gauchada.Backend.Model.Entity
{
    public class PassengerEntity : UserAbstract
    {
        public PassengerEntity() { }
        public PassengerEntity(PassengerDTO passenger)
        : base(passenger.UserName, passenger.Name, passenger.LastName, passenger.Email, passenger.Birth, passenger.PhoneNumber)
        {
        }
    }
}
