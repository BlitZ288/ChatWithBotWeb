using Coman;
using System;

namespace ChatWithBotWeb.Models
{
    public class LogAction
    {
        public int LogActionId { get; set; }
        public DateTime FixLog { get; set; }
        public EventChat Content { get; set; }
        public int? ChatId { get; set; }
        public bool Undread { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }

        public LogAction(DateTime fixLog, EventChat content, User user)
        {
            FixLog = fixLog;
            Content = content;
            User = user;
            Undread = true;
        }
        public LogAction()
        {

        }
    }
}
