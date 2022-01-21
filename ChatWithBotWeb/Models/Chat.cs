using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatWithBotWeb.Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public List<Message> ListMessage { get; set; } = new List<Message>();

        public List<LogAction> LogActions { get; set; } = new List<LogAction>();

        public List<LogsUser> ChatLogUsers { get; set; } = new List<LogsUser>();
        public List<string> NameBots { get; set; } = new List<string>();

        public Chat(User user, string name)
        {
            Name = name;
            Users.Add(user);
            ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null });
        }
        public Chat()
        {
        }
    }
}
