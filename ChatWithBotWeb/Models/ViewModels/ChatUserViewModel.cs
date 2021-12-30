using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.ViewModels
{
    public class ChatUserViewModel
    {
        public Chat Chat { get; set; }
        public User CurrentUser { get; set; }
        public List<User> UsersNotInclude { get; set; }
        public List<Message> HistoryChat{ get; set; }
        public List<string> NameBot{ get; set; }
    }
}
