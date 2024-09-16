using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Model.Entity
{
    public class DriverMessage
    {
        [Key] [Required] public int DriverMessageId { get; set; }
        [Required] public string MessageContent { get; set; }
        [Required] public DateTime WriteTime { get; set; }
        [Required] public int ChatId { get; set; }
        [Required] public string WriterUsername { get; set; }
        public DriverEntity Writer { get; set; }
        public Chat Chat { get; set; }
    }
}
