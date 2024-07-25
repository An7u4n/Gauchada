using Microsoft.AspNetCore.Http;

namespace Gauchada.Backend.Model.DTO
{
    public class AddUserDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Photo { get; set; }

        public AddUserDTO() { }

        public AddUserDTO(string userName, string name, string lastName, string email, DateTime birth, string phoneNumber, IFormFile photo)
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
