using Coman.InterfaceBots;
using System.Collections.Generic;

namespace BotService
{
    public interface IBotsRepository
    {
        /// <summary>
        /// Получить список всех ботов 
        /// </summary>
        /// <returns></returns>
        IEnumerable<IBot> GetAllBots();
    }
}
