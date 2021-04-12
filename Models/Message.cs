using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Models
{
    public class Message
    {
        public Message(string user, string messageContent)
        {
            User = user;
            MessageContent = messageContent;
        }

        public string MessageContent { get; private set; }

        public string User { get; private set; }
    }
}
