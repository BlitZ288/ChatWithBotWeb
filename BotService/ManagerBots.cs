using Coman;
using Coman.InterfaceBots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotService
{
    public class ManagerBots
    {
        private readonly IEnumerable<IMessageBot> messageBots;
        private readonly IEnumerable<IEventBot> eventBots;
        private readonly IEnumerable<string> listNameBots;
        private bool isRunning;
        public ManagerBots()///BotsManger
        {
           
        }
        public void Start()
        {
            if (isRunning)
            {
                throw new Exception("Вы пытаетесь повторно запустить BotsManager");
            }
            BotsRepository botFactory = new BotsRepository();
            botFactory.Init(); 
            this.messageBots = botFactory.ListMessageBots;
            this.eventBots = botFactory.ListEventBots;
            this.listNameBots = botFactory.ListNameBots;
        }
        public IEnumerable<string> GetAllNameBots()
        {
            return listNameBots;
        }
        public BotAnswer GetMessage(string message, IEnumerable<string> nameChatBots)
        {
            var availableBots = this.messageBots.Where(b=>nameChatBots.Contains(b.NameBot));
            var messageBotsCount = availableBots.Count();
            if (messageBotsCount < 0) return null;

            var botsTasks = new List<Task<string>>();

         
            foreach (var messageBot in this.messageBots)
            {
                botsTasks.Add(Task.Run(() => messageBot.Move(message)));
            }

            int indexTask = Task.WaitAny(botsTasks.ToArray());
            var contentAnswer = botsTasks[indexTask].Result;

            if (String.IsNullOrEmpty(contentAnswer)) return null;

            BotAnswer botAnswer = new BotAnswer(contentAnswer,messageBots.ElementAt(indexTask).NameBot);

            return botAnswer;           
            /*
              1. Клиент пишет сообщение отправляет на сервер 
              2. Сообщение клиента оборачивается в экземпляр Message 
              3. Данный экз. передается в текущий метод 
              4. Создаю экзп. ботов которым будет передано сообщение на обработку  
              5. Происходит делегирование сообщение на обработку бота 
              6. Берем первое сообщение который вернет произвольный бот 
             */
        }
        public BotAnswer GetEventMessage(EventChat eventChat, IEnumerable<string> nameChatBots)
        {
            var availableBots = this.eventBots.Where(b => nameChatBots.Contains(b.NameBot));
            var messageBotsCount = availableBots.Count();
            if (messageBotsCount < 0) return null;

            var botsTasks = new List<Task<string>>();

            foreach (var eventBot in this.eventBots)
            {
                botsTasks.Add(Task.Run(() => eventBot.Move(eventChat)));
            }

            int indexTask = Task.WaitAny(botsTasks.ToArray());
            var contentAnswer = botsTasks[indexTask].Result;

            BotAnswer botAnswer = new BotAnswer(contentAnswer, messageBots.ElementAt(indexTask).NameBot);

            return botAnswer;
           
        }
    }
}
