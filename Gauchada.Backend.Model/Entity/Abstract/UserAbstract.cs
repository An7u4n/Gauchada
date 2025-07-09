using System.ComponentModel.DataAnnotations;

namespace Gauchada.Backend.Model.Entity.Abstract
{
    public abstract class UserAbstract
    {
        [Key] [Required][MaxLength(32)] public required string UserName { get; set; }
        [Required][MaxLength(32)] public required string Name { get; set; }
        [Required][MaxLength(32)] public required string LastName { get; set; }
        [Required][EmailAddress] public required string Email { get; set; }
        public DateTime Birth { get; set; }
        [Required][Phone] public required string PhoneNumber { get; set; }
        [MaxLength(255)] public string? PhotoSrc { get; set; }

        protected UserAbstract() { }

        protected UserAbstract(string userName, string name, string lastName, string email, DateTime birth, string phoneNumber, string? photoSrc)
        {
            UserName = userName;
            Name = name;
            LastName = lastName;
            Email = email;
            Birth = birth;
            PhoneNumber = phoneNumber;
            PhotoSrc = photoSrc;
        }
    }
}
