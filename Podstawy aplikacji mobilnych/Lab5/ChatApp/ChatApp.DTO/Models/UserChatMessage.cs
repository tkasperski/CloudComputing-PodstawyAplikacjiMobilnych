using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DTO.Models
{
    public class UserChatMessage
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public string TimeStampString => TimeStamp.ToString("dd-MM-yyyy, hh:mm:ss");
    }
}
