using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Interface
{
    public interface IRepositoryUser
    {
        List<User> GetAllUsers { get; }
        void AddUser(User user);
        User GetUser(string indexUser);
        void DeleteUser(User user);
    }
}
