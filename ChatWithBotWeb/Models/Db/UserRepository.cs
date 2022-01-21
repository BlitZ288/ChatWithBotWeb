using ChatWithBotWeb.Models.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ChatWithBotWeb.Models.Db
{
    class RepositoryUser : IRepositoryUser
    {
        private ApplicationDbContext context;
        public RepositoryUser(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<User> GetAllUsers => context.Users.ToList();

        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User GetUser(string indexUser)
        {
            return context.Users.FirstOrDefault(c => c.Id == indexUser);
        }
        public void DeleteUser(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
