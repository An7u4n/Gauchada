using System.ComponentModel.DataAnnotations;

namespace Gauchada.Backend.Model.Entity.Abstract
{
    public abstract class UserAbstract
    {
        [Key] [Required][MaxLength(32)] public string UserName { get; set; }
        [Required][MaxLength(32)] public string Name { get; set; }
        [Required][MaxLength(32)] public string LastName { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public DateTime Birth { get; set; }
        [Required][Phone] public string PhoneNumber { get; set; }

        protected UserAbstract() { }

        protected UserAbstract(string userName, string name, string lastName, string email, DateTime birth, string phoneNumber)
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
