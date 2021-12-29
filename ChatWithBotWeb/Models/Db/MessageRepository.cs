using ChatWithBotWeb.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Db
{
     class MessageRepository : IRepositoryMessage
    {
        private ApplicationDbContext Context;
        public MessageRepository (ApplicationDbContext context)
        {
            Context = context;
        }

        List<Message> IRepositoryMessage.GetMessages => Context.Messages.Include(m=>m.Chat).Include(m=>m.User).ToList();

        public void AddMessages(List<Message> message)
        {
            Context.AddRange(message);
            Context.SaveChanges();
        }


        public List<Message> UnreadMessages()
        {
           return Context.Messages.Include(m => m.Chat).Include(m => m.User).Where(m => m.Undread == true).ToList();
        }
    }
}
