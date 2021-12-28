using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public List<LogsUser> LogsUsers { get; set; } = new List<LogsUser>();

        public User(string name)
        {
           Name = name;
        }
        public User(string name,string password)
        {
            Name = name;
            Password = password;
        }
        public User() { }
        /// <summary>
        /// Добавление в чат 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="NameInvite"></param>
        //public void SignUser(Chat chat, User user)
        //{
        //    if (!chat.ChatLogUsers.ContainsKey(user))
        //    {
        //      chat.ChatLogUsers.Add(user, new LogsUser() { StartChat = DateTime.Now, StopChat = null });
        //    }
        //    else
        //    {
        //      chat.ChatLogUsers[user].StopChat = null;
        //    }
        //    chat.Users.Add(user);
        //}
        /// <summary>
        /// Удалить из чата 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="user"></param>
        /// <param name="NameInvite"></param>
        //public void LogutUser(Chat chat, User user, string nameDelete)
        //{
        //    var CickUer = chat.Users.Where(u => u.Name == nameDelete).FirstOrDefault();
        //    if (CickUer != null)
        //    {
        //        chat.ChatLogUsers[CickUer].StopChat = DateTime.Now;
        //        chat.Users.Remove(CickUer);
        //    }
        //    else
        //    {
        //        throw new ArgumentNullException("Пользователя с таким именем нет");
        //    }

        //}
        /// <summary>
        /// Удалить сообщение 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="index"></param>
        /// <param name="user"></param>
        public void DelMessage(Chat chat, int index, User user)
        {
            Message ChoiceMessage = chat.ListMessage[index];
            if (ChoiceMessage.User.Equals(user) && DateTime.Now.Subtract(ChoiceMessage.dateTime) < new TimeSpan(60, 0, 0))
            {
                chat.ListMessage.Remove(ChoiceMessage);
            }
            else
            {
                throw new ArgumentException("Вы не можете удалить это сообщение. Вы не являетесь его владельцем либо прошло слишком много времени");

            }
        }
        /// <summary>
        /// Удалить чат 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="user"></param>
        //public bool DelChat(Chat chat, User user, List<Chat> chats)
        //{
        //    if (chat.Users.Contains(user))
        //    {
        //        chat.ChatLogUsers[user].StopChat = DateTime.Now;
        //        chat.Users.Remove(user);
        //        chats.Remove(chat);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public override bool Equals(object obj)
        {
            return (obj == null) || (obj.GetType() != typeof(User))
            ? false
            : Name == ((User)obj).Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
