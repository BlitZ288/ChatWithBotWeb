using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ChatWithBotWeb.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public List<LogsUser> LogsUsers { get; set; } = new List<LogsUser>();
        public List<Chat> Chats { get; set; } = new List<Chat>();
        public User(string name)
        {
            Name = name;
        }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public User() { }
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
