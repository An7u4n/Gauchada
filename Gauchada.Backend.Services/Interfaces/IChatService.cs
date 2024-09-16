using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Services.Interfaces
{
    public interface IChatService
    {
        public Task<ChatDTO> GetChatMessages(int tripId);
    }
}
