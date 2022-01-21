using ChatWithBotWeb.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChatWithBotWeb.Models.Db
{
    class LogActionRepository : IRepositoryLogAction
    {
        private ApplicationDbContext context;
        public LogActionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddLog(LogAction logsAction)
        {
            context.LogActions.Add(logsAction);
            context.SaveChanges();
        }

        public void DeleteLog(LogAction logsAction)
        {
            context.LogActions.Remove(logsAction);
            context.SaveChanges();
        }

        public List<LogAction> GetAll()
        {
            return context.LogActions.Include(l => l.Chat).Include(l => l.User).ToList();
        }

        public List<LogAction> GetLogUnderRead()
        {
            return context.LogActions.Include(l => l.Chat).Include(l => l.User).Where(l => l.Undread == true).ToList();

        }

        public void Update(List<LogAction> logsAction)
        {
            context.SaveChanges();
        }

        public void Update(LogAction logsAction)
        {
            context.SaveChanges();
        }
    }
}
