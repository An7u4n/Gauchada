namespace Gauchada.Backend.Model.Entity
{
    public class PassengerEntity : UserAbstract
    {
        public PassengerEntity(string userName, string name, string lastName, string email, DateTime birth, string phoneNumber, byte[] photo)
            : base(userName, name, lastName, email, birth, phoneNumber, photo) { }
    }
}
