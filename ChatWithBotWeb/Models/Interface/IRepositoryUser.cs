using System.Collections.Generic;

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
