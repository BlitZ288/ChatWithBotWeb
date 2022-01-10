using ChatWithBotWeb.Models.Interface;
using Microsoft.EntityFrameworkCore;
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
        public List<Chat> GetAllChat
        {
            get
            {
                var ListChat= Context.Chats.Include(c => c.Users).Include(c => c.ListMessage).Include(c => c.ChatLogUsers).Include(c => c.LogActions).ToList();
                foreach(var chat in ListChat)
                {
                    if (chat.NameBots!=null)
                    {
                        foreach(var name in chat.NameBots)
                        {
                            try
                            {
                                Type type = Type.GetType("ChatWithBotWeb.Models.Bots." + name);
                                IBot bot = (IBot)Activator.CreateInstance(type);
                                chat.ChatBot.Add(bot);
                            }
                            catch
                            {
                             
                            }
                            
                        }
                    }
                }
                return ListChat;
            }
        }

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
          Chat chat=  Context.Chats.Include(c => c.Users).Include(c => c.ListMessage)
                                                         .Include(c => c.ChatLogUsers)
                                                         .FirstOrDefault(c => c.ChatId == indexChat);
                if (chat.NameBots != null)
                {
                    foreach (var name in chat.NameBots)
                    {
                    try
                    {   /*Подумать как сделать джинерик по умному */
                        Type type = Type.GetType("ChatWithBotWeb.Models.Bots." + name);
                        IBot bot = (IBot)Activator.CreateInstance(type);
                        chat.ChatBot.Add(bot);
                    }
                    catch
                    {

                    }
                }
                }
            
            return chat;
        }
        public Chat DeleteUserChat(Chat chat, User user)
        {
            Chat dbEntry = Context.Chats.Include(c => c.Users).Include(c => c.ListMessage)
                                                              .Include(c => c.ChatLogUsers)
                                                              .FirstOrDefault(c => c.ChatId == chat.ChatId);            
            dbEntry.Users.Remove(user);
            Context.SaveChanges();
            return dbEntry;

        }
        public Chat AddUserChat(Chat chat , User user)
        {
            Chat dbEntry = Context.Chats.Include(c => c.Users).Include(c => c.ListMessage)
                                                              .Include(c => c.ChatLogUsers)
                                                               .FirstOrDefault(c => c.ChatId == chat.ChatId);
            dbEntry.Users.Add(user);
            Context.SaveChanges();
            return dbEntry;
        }
        public void UpdateChat(Chat chat)
        {
            Context.SaveChanges();
        }
    }
}
