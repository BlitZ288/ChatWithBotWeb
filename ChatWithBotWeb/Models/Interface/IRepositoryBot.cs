using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Interface
{
    public interface IRepositoryBot
    {
        List<IBot> Bots { get; }
        List<string> BotNames { get; }
    }
}
