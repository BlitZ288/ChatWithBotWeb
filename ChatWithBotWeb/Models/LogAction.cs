using ChatWithBotWeb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models
{
    /// <summary>
    /// Логирует действия в чате 
    /// </summary>
     public class LogAction
    {
       public int LogActionId { get; set; }
       public DateTime FixLog { get; set; }
       public EventChat Content { get; set; }
       public int? ChatId { get; set; }
       public bool Undread { get; set; }
       public User User{ get; set; }
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
