using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Bots
{
    public interface IBotEvent:IBot
    {
        new string Move(object command);
    }
}
