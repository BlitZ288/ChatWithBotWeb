using ChatWithBotWeb.Models.Bots;
using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Db
{
    public class BotsRepository : IRepositoryBot
    {
        public List<IBot> Bots => new List<IBot>() { 
            new BotAnecdote(),
            new BotTime(),
        };
    }
}
