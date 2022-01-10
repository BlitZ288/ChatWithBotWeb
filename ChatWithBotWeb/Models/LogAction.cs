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
       public string Content { get; set; }
       public User User{ get; set; }
       public Chat Chat { get; set; }
      
        public LogAction(DateTime fixLog, string content, User user)
        {
            FixLog = fixLog;
            Content = content;
            User = user;
        }
        public LogAction()
        {

        }
    }
}
