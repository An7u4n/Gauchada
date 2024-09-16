using Gauchada.Backend.Model.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Model.Entity
{
    public class Chat
    {
        [Key] [Required] public int ChatId { get; set; }
        [Required] public bool Active { get; set; }
        [Required] public int TripId { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<DriverMessage> DriverMessages { get; set; } = new List<DriverMessage>();
        public TripEntity Trip { get; set; }
    }
}
