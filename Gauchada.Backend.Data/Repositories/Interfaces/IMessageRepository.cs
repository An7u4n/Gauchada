using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Data.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        public Task CreateMessage(int chatId, string message, string writer, string userType);
    }
}
