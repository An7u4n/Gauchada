using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Services.Interfaces
{
    public interface IMessageService
    {
        public Task SendMessage(int chatId, string messageContent, string writerUsername, string userType);
    }
}
