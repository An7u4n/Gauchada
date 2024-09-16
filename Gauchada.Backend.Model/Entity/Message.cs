using Gauchada.Backend.Model.Entity.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Gauchada.Backend.Model.Entity
{
    public class Message
    {
        [Key] [Required] public int MessageId { get; set; }
        [Required] public string MessageContent { get; set; }
        [Required] public DateTime WriteTime { get; set; }
        [Required] public int ChatId { get; set; }
        [Required] public string WriterUsername { get; set; }
        public PassengerEntity Writer { get; set; }
        public Chat Chat { get; set; }
    }
}
