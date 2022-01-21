using System.Collections.Generic;

namespace ChatWithBotWeb.Models
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string NameChat { get; set; }
        public List<User> Users { get; set; }
        public IEnumerable<string> NameBots { get; set; }
    }
}
