using ChatWithBotWeb.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Db
{
    class ChatRepository : IRepositoryChat
    {
        private ApplicationDbContext context;
        public ChatRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<Chat> GetAllChat
        {
            get
            {
                var ListChat = context.Chats.Include(c => c.Users).Include(c => c.ListMessage).Include(c => c.ChatLogUsers).Include(c => c.LogActions).ToList();

                return ListChat;
            }
        }

        public void AddChat(Chat chat)
        {
            context.Chats.Add(chat);
            context.SaveChanges();
        }

        public void DeleteChat(Chat chat)
        {
            context.Chats.Remove(chat);
            context.SaveChanges();
        }

        public Chat GetChat(int indexChat)
        {
            Chat chat = context.Chats.Include(c => c.Users).Include(c => c.ListMessage).Include(c => c.LogActions)
                                                                                       .Include(c => c.ChatLogUsers)
                                                                                       .FirstOrDefault(c => c.ChatId == indexChat);
            return chat;
        }
        public Chat DeleteUserChat(Chat chat, User user)
        {
            Chat dbEntry = context.Chats.Include(c => c.Users).Include(c => c.ListMessage)
                                                              .Include(c => c.ChatLogUsers)
                                                              .FirstOrDefault(c => c.ChatId == chat.ChatId);
            dbEntry.Users.Remove(user);
            context.SaveChanges();
            return dbEntry;

        }
        public Chat AddUserChat(Chat chat, User user)
        {
            Chat dbEntry = context.Chats.Include(c => c.Users).Include(c => c.ListMessage)
                                                              .Include(c => c.ChatLogUsers)
                                                              .FirstOrDefault(c => c.ChatId == chat.ChatId);
            dbEntry.Users.Add(user);
            context.SaveChanges();
            return dbEntry;
        }
        public void UpdateChat(Chat chat)
        {
            context.SaveChanges();
        }
        public async Task UpdateChatAsync(Chat chat)
        {
            await context.SaveChangesAsync();
        }
    }
}
