using Domian.Entities;
using System.Collections.Generic;


namespace ChatWithBotWeb.Models.ViewModels
{
    public class ChatUserViewModel
    {
        public int ChatId { get; set; }
        public User CurrentUser { get; set; }
        public IEnumerable<User> UsersNotInclude { get; set; }
        public IEnumerable<User> ChatUsers { get; set; }
        public IEnumerable<Message> HistoryChat { get; set; }
        public IEnumerable<string> AvailableBots { get; set; }
        public IEnumerable<string> ChatBots { get; set; }
    }
}
