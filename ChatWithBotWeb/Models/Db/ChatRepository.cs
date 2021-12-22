using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Db
{
    class ChatRepository : IRepositoryChat
    {
        private ApplicationDbContext Context;
        public ChatRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public List<Chat> GetAllChat =>Context.Chats.ToList();

        public void AddChat(Chat chat)
        {
            Context.Chats.Add(chat);
            Context.SaveChanges();
        }

        public void DeleteChat(Chat chat)
        {
            Context.Chats.Remove(chat);
            Context.SaveChanges();
        }

        public Chat GetChat(int indexChat)
        {
            return Context.Chats.FirstOrDefault(c => c.ChatId == indexChat);
        }
    }
}
