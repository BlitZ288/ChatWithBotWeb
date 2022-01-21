using ChatWithBotWeb.Models.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ChatWithBotWeb.Models.Db
{
    class LogUserRepository : IRepositoryLogUser
    {
        private ApplicationDbContext context;
        public LogUserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddLog(LogsUser logsUser)
        {
            context.LogsUsers.Add(logsUser);
            context.SaveChanges();
        }

        public void DeleteLog(LogsUser logsUser)
        {
            context.LogsUsers.Remove(logsUser);
            context.SaveChanges();
        }

        public List<LogsUser> GetAll()
        {
            return context.LogsUsers.ToList();
        }

        public LogsUser GetLog(User user, Chat chat)
        {
            return context.LogsUsers.FirstOrDefault(l => l.User.Id == user.Id && l.Chat == chat);
        }
    }
}
