using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Db
{
    class LogUserRepository: IRepositoryLogUser
    {
        private ApplicationDbContext Context;
        public LogUserRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public void AddLog(LogsUser logsUser)
        {
            Context.LogsUsers.Add(logsUser);
            Context.SaveChanges();    
        }

        public void DeleteLog(LogsUser logsUser)
        {
            Context.LogsUsers.Remove(logsUser);
            Context.SaveChanges();
        }

        public List<LogsUser> GetAll()
        {
            return Context.LogsUsers.ToList();
        }

        public LogsUser GetLog(User user, Chat chat)
        {
            return Context.LogsUsers.FirstOrDefault(l => l.User.Id == user.Id && l.Chat == chat);
        }
    }
}
