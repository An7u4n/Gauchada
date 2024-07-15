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
    }
}
