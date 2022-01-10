using ChatWithBotWeb.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Db
{
    class LogActionRepository : IRepositoryLogAction
    {
        private ApplicationDbContext Context;
        public LogActionRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public void AddLog(LogAction logsAction)
        {
            Context.LogActions.Add(logsAction);
            Context.SaveChanges();
        }

        public void DeleteLog(LogAction logsAction)
        {
            Context.LogActions.Remove(logsAction);
            Context.SaveChanges();
        }

        public List<LogAction> GetAll()
        {
            return Context.LogActions.Include(l => l.Chat).Include(l => l.User).ToList();
        }

        public List< LogAction> GetLogUnderRead(Chat chat)
        {
            return Context.LogActions.Include(l => l.Chat).Include(l => l.User).Where(l=>l.Undread==true).ToList();

        }
    }
}
