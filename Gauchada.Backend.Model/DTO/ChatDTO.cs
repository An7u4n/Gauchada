using Gauchada.Backend.Model.Entity;

namespace Gauchada.Backend.Model.DTO
{
    public class ChatDTO
    {
        public int ChatId { get; set; }
        public List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
        public ChatDTO(Chat chat)
        {
            ChatId = chat.ChatId;
            Messages = chat.Messages.Select(m => new MessageDTO(m)).ToList();
            chat.DriverMessages.ToList().ForEach(m => Messages.Add(new MessageDTO(m)));
            Messages.OrderBy(m => m.WriteTime);
        }
    }
}
