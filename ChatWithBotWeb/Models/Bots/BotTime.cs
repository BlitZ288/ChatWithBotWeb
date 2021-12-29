using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Bots
{
    public class BotTime : IBot
    {
        public string NameBot
        {
            get
            {
                return "BotTime";
            }
            set
            {

            }

        }
        public string Move(string command)
        {
            try
            {
                if (command.Equals("ВРЕМЯ"))
                {
                    return DateTime.Now.ToString("HH:mm");
                }
                else
                {
                    return "";
                }
                //if(command.Equals("ЧЕРЕЗ-"))
                //{
                //    return DateTime.Now.AddMinutes(Convert.ToDouble(command)).ToString("HH:mm");
                //}
            }
            catch
            {
                return String.Empty;
            }

        }
        public StringBuilder GetAllCommand()
        {
            StringBuilder command = new StringBuilder("");
            command.Append("/current-Узнать текущие время");
           // command.Append("\n" + "/Через time");
            return command;
        }
    }
}
