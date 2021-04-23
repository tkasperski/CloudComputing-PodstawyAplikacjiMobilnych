using ChatApp.DTO;
using ChatApp.DTO.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(UserChatMessage message)
        {
            message.TimeStamp = DateTime.Now;
            await Clients.All.SendAsync(Consts.RECEIVED_MESSAGE, message);
        }
    }
}
