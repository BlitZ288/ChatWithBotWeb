using ChatWithBotWeb.Models.Bots;
using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Db
{
    public class BotRepository : IRepositoryBot
    {
        public List<IBot> GetAllBots => new List<IBot>() { 
            new BotAnecdote(),
            new BotTime(),
            new BotEvent()
        };
        public List<string> GetAllNameBots => new List<string>()
        {
             new BotAnecdote().NameBot,
             new BotTime().NameBot,
             new BotEvent().NameBot
        };
        public List<IBot> GetMessageBots => new List<IBot>() {
            new BotAnecdote(),
            new BotTime(),
         
        };
        public List<IBot> GetEventBots()
        {
            return new List<IBot>() { new BotEvent() };
        }

        public List<string> GetNameEventBots()
        {
            return new List<string>() { new BotEvent().NameBot };
        }

        public List<string> GetNameMessageBots()
        {
            return new List<string>() { new BotAnecdote().NameBot, new BotTime().NameBot };
        }
    }
}
