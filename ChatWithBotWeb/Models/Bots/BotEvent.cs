using ChatWithBotWeb.Infrastructure;
using ChatWithBotWeb.Models.Interface;
using ChatWithBotWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Bots
{
    public class BotEvent : IBot
    {
        public string NameBot
        {
            get
            {
                return "BotEvent";
            }
            set
            {

            }

        }
        public  StringBuilder GetAllCommand()
        {
            throw new NotImplementedException();
        }

        public  string Move(object command)
        {
            string result = "";
            switch (command)
            {
                case EventChat.JoinChat:
                    result = "Добро пожаловать в чат ";
                    break;
                case EventChat.DeletePerson:
                    result = "Пользователь вышел из чата ";
                    break;
                case EventChat.DeleteBot:
                    result = "Бот удален";
                    break;
            }
            return result;
        }

        
    }
}
