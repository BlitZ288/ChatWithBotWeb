using ChatWithBotWeb.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChatWithBotWeb.Models.Db
{
    class MessageRepository : IRepositoryMessage
    {
        private ApplicationDbContext context;
        public MessageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        List<Message> IRepositoryMessage.GetMessages => context.Messages.Include(m => m.Chat).Include(m => m.User).ToList();

        public void AddMessages(List<Message> message)
        {
            context.AddRange(message);
            context.SaveChanges();
        }


        public List<Message> UnreadMessages()
        {
            return context.Messages.Include(m => m.Chat).Include(m => m.User).Where(m => m.Undread == true).ToList();
        }
    }
}
