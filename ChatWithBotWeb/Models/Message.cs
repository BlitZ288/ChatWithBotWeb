using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models
{
    public  class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime dateTime { get; set; }
        public string OutUser { get; set; }
        public User User;
        public Chat Chat { get; set; }

        public Message(string content, string nameSend, Chat chat, User user)
        {
            var Senduser = chat.Users.FirstOrDefault(c => c.Name.Equals(nameSend));
            var SendBot = chat.ChatBot.FirstOrDefault(c => c.NameBot.Equals(nameSend));

            if (Senduser != null || SendBot != null)
            {
                if (chat.ListMessage.Any())
                {
                    MessageId = chat.ListMessage[^1].MessageId + 1;
                }
                else
                {
                    MessageId = 0;
                }
                Content = content;
                dateTime = DateTime.Now;
                OutUser = Senduser == null ? SendBot.NameBot : Senduser.Name;
                User = user;
            }
            else
            {
                throw new ArgumentNullException("Этого пользователя нет в чате");
            }
        }
        public Message()
        {

        }
    }
}
