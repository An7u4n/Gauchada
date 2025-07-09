using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity.Abstract;

namespace Gauchada.Backend.Model.Entity
{
    public class PassengerEntity : UserAbstract
    {

        public virtual ICollection<TripEntity> Trips { get; set; } = new List<TripEntity>();
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public PassengerEntity() { }
        public PassengerEntity(AddUserDTO passenger, string? photoSrc)
        : base(passenger.UserName, passenger.Name, passenger.LastName, passenger.Email, passenger.Birth, passenger.PhoneNumber, photoSrc)
        {
        }
    }
}
