using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models
{
    /// <summary>
    /// Логирует вступление в чат и выход из него
    /// </summary>
    [Serializable]
    public class LogsUser
    {
       public int LogsUserId { get; set; }
       public DateTime StartChat { get; set; }
       public DateTime? StopChat { get; set; }
       public int? ChatId { get; set; }
       public Chat Chat { get; set; }
       public User User { get; set; }  
    }
}
