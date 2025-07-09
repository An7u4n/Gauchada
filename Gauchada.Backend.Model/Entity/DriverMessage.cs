using System.ComponentModel.DataAnnotations;

namespace Gauchada.Backend.Model.Entity
{
    public class DriverMessage
    {
        [Key] public int DriverMessageId { get; set; }
        [Required] public required string MessageContent { get; set; }
        public DateTime WriteTime { get; set; }
        public int ChatId { get; set; }
        [Required] public required string WriterUsername { get; set; }
        public DriverEntity? Writer { get; set; }
        public Chat? Chat { get; set; }
    }
}
