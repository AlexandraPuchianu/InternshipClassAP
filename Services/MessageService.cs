using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Models;

namespace InternshipClass.Services
{
    public class MessageService
    {
        private readonly List<Message> messagesList;

        public MessageService()
        {
            messagesList = new List<Message>();
        }

        public List<Message> GetAllMessages()
        {
            return messagesList;
        }

        public void AddMessage(string user, string messageContent)
        {
            Message newMessage = new Message(user, messageContent);
            messagesList.Add(newMessage);
        }
    }
}
