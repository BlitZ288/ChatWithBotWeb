using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models
{
    [Serializable]

    public class Chat
    {
        public int ChatId { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
      
        public List<Message> ListMessage { get; set; } = new List<Message>();

        public List<LogAction> LogActions { get; set; } = new List<LogAction>();

        public List<LogsUser> ChatLogUsers { get; set; } = new List<LogsUser>();

        public List<IBot> ChatBot = new List<IBot>();
        public Chat(User user, string name)
        {
            Name = name;
            Users.Add(user);
            ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null });
        }
        public Chat()
        {
        }

        public void AddLogChat(Chat chat , string eventChat, User user)
        {
            chat.LogActions.Add(new LogAction(DateTime.Now, eventChat, user.Name));          
        }
        //public List<Message> GetHistoryChat(Chat chat, User user)
        //{
        //    List<Message> result = new List<Message>();
        //    try
        //    {
        //        if (chat.ChatLogUsers[user] != null)
        //        {
        //            var mes = chat.ListMessage.Where(m => chat.ChatLogUsers[user].StartChat <= m.dateTime);
        //            if (chat.ChatLogUsers[user].StopChat != null)
        //            {
        //                mes = chat.ListMessage.Where(m => m.dateTime < chat.ChatLogUsers[user].StopChat);
        //            }
        //            if (mes.Any())
        //            {
                       
        //                return mes.ToList();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //         return result;
        //    }
        //    return result;
        //}

        //public Message SendMessege(User user, string contetn)
        //{
        //   var message = new Message(contetn, user);
        //   return message;
        //        //else
        //        //{
        //        //    throw new Exception("Вы не являись участником \n Чтобы присоединиться используйте команду sign @usernames");
        //        //}
        //}
        //public void InviteUser(Chat chat, User user )
        //{
        //    if (!chat.ChatLogUsers.ContainsKey(user))
        //    {
        //        chat.ChatLogUsers.Add(user, new LogsUser() { StartChat = DateTime.Now, StopChat = null });
        //    }
        //    else
        //    {
        //        chat.ChatLogUsers[user].StopChat = null;
        //    }
        //    chat.Users.Add(user);
        //}
        //public void DeleteUser(Chat chat, User user)
        //{
        //    chat.ChatLogUsers[user].StopChat = DateTime.Now;
        //    chat.Users.Remove(user);
        //}
    }
}
