using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Interface
{
    public interface IRepositoryBot
    {
        List<IBot> GetAllBots { get; }
        List<string> GetAllNameBots { get; }
        List<IBot> GetMessageBots { get; }
        List<string> GetNameMessageBots();
        List<IBot> GetEventBots();
        List<string> GetNameEventBots();
       
    }
}
