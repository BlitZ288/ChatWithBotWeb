﻿using ChatWithBotWeb.Models.Interface;
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
              return  Context.Chats.Include(c => c.Users).Include(c => c.ListMessage).ToList();
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
                                                         .FirstOrDefault(c => c.ChatId == indexChat);
            return chat;
        }
    }
}
