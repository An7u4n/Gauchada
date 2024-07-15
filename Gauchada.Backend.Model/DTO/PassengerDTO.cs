namespace Gauchada.Backend.Model.DTO
{
    public class PassengerDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }
        public string PhoneNumber { get; set; }
        //public byte[] Photo { get; set; }

        public PassengerDTO(string userName, string name, string lastName, string email, DateTime birth, string phoneNumber)
        {
            UserName = userName;
            Name = name;
            LastName = lastName;
            Email = email;
            Birth = birth;
            PhoneNumber = phoneNumber;
        }
        public override bool Equals(object obj)
        {
            var other = obj as PassengerDTO;

            if (other == null)
                return false;

            return this.UserName == other.UserName &&
                   this.Name == other.Name &&
                   this.LastName == other.LastName &&
                   this.Email == other.Email &&
                   this.Birth == other.Birth &&
                   this.PhoneNumber == other.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserName, Name, LastName, Email, Birth, PhoneNumber);
        }
    }
}
