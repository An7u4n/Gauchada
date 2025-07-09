using System.ComponentModel.DataAnnotations;

namespace Gauchada.Backend.Model.Entity
{
    public class Message
    {
        [Key] [Required] public int MessageId { get; set; }
        [Required] public required string MessageContent { get; set; }
        [Required] public required DateTime WriteTime { get; set; }
        [Required] public required int ChatId { get; set; }
        [Required] public required string WriterUsername { get; set; }
        public PassengerEntity? Writer { get; set; }
        public Chat? Chat { get; set; }
    }
}
