using Domian.Entities;
using System.Collections.Generic;

namespace ChatWithBotWeb.Service.UserService.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser();
        IEnumerable<User> GetAllUsersByChat(int chatId);
        IEnumerable<string> GetAllBotsName();
        IEnumerable<string> GetBotsNameByIdChat(int chatId);

        User GetUser(int id);
        User GetUserByName(string name);

        void CreateUser(User user);
        void Dispose();
    }
}
