namespace Gauchada.Backend.Model.Entity
{
    public abstract class UserAbstract
    {
        public readonly string UserName;
        public readonly string Name;
        public readonly string LastName;
        public readonly string Email;
        public readonly DateTime Birth;
        public readonly string PhoneNumber;
        public readonly byte[] Photo;

        public UserAbstract(string userName, string name, string lastName, string email, DateTime birth, string phoneNumber, byte[] photo)
        {
            UserName = userName;
            Name = name;
            LastName = lastName;
            Email = email;
            Birth = birth;
            PhoneNumber = phoneNumber;
            Photo = photo;
        }
    }
}
