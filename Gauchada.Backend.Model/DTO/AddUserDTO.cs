using Microsoft.AspNetCore.Http;

namespace Gauchada.Backend.Model.DTO
{
    public class AddUserDTO
    {
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required DateTime Birth { get; set; }
        public required string PhoneNumber { get; set; }
        public IFormFile? Photo { get; set; }

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
