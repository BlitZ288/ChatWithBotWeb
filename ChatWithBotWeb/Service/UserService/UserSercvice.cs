using ChatWithBotWeb.Service.UserService.Interface;
using Domian;
using Domian.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ChatWithBotWeb.Service.UserService
{
    public class UserSercvice : IUserService
    {
        IUnitOfWorck Database { get; set; }

        public UserSercvice(IUnitOfWorck unit)
        {
            Database = unit;
        }
        public void CreateUser(User user)
        {
            Database.Users.Create(user);
            Database.Save();
        }

        public IEnumerable<User> GetAllUser()
        {
            return Database.Users.GetAll();
        }

        public IEnumerable<User> GetAllUsersByChat(int chatId)
        {
            return Database.Users.GetAll().Where(u => u.ChatId == chatId && u.TypeUser == TypeUser.Persone);
        }
        public IEnumerable<string> GetAllBotsName()
        {
            return Database.Users.GetAll().Where(u => u.TypeUser == TypeUser.Bot).Select(u => u.Name);
        }

        public IEnumerable<string> GetBotsNameByIdChat(int chatId)
        {
            return Database.Users.GetAll().Where(u => u.ChatId == chatId && u.TypeUser == TypeUser.Bot).Select(u => u.Name);
        }

        public User GetUser(int id)
        {
            return Database.Users.Get(id);
        }
        public User GetUserByName(string name)
        {
            return Database.Users.GetAll().FirstOrDefault(u => u.Name == name);
        }
        public void Dispose()
        {
            Database.Dispose();
        }


    }
}
