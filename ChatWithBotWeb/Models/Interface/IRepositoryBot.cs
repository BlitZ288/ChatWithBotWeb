using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Interface
{
    interface IRepositoryBot
    {
        List<IBot> Bots { get; }
    }
}
