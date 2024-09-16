using Gauchada.Backend.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Model.DTO
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; } = string.Empty;
        public DateTime? WriteTime { get; set; }
        public string WriterUsername { get; set; } = string.Empty;

        public MessageDTO(DriverMessage message) 
        {

            MessageId = message.DriverMessageId;
            MessageContent = message.MessageContent;
            WriteTime = message.WriteTime;
            WriterUsername = message.WriterUsername;
        }

        public MessageDTO(Message message)
        {

            MessageId = message.MessageId;
            MessageContent = message.MessageContent;
            WriteTime = message.WriteTime;
            WriterUsername = message.WriterUsername;
        }
    }
}
