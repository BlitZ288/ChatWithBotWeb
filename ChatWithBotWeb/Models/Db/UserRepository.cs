using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Db
{
    class RepositoryUser:IRepositoryUser
    {
        private ApplicationDbContext Context;
        public RepositoryUser  (ApplicationDbContext context)
        {
            Context = context;
        }
       
        public List<User> GetAllUsers => Context.Users.ToList();

        public void AddUser(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }
        public User GetUser(string indexUser)
        {
            return Context.Users.FirstOrDefault(c => c.Id == indexUser);
        }
        public void DeleteUser(User user)
        {
            Context.Users.Remove(user);
            Context.SaveChanges();
        }
    }
}
