using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Db;
using ChatWithBotWeb.Models.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Services
{
    public class BotServices : IHostedService
    {
        private IServiceProvider provider;
        public BotServices(IServiceProvider Serviceprovider)
        {
            provider = Serviceprovider;
        }
        public  Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => DoWork(),cancellationToken).ConfigureAwait(false);
            return Task.CompletedTask;
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
        public async Task DoWork ()
        {
            while (true)
            {
                using(var scope = provider.CreateScope())
                {
                    var efContextMessage = scope.ServiceProvider.GetService<IRepositoryMessage>();
                    var efContextAction = scope.ServiceProvider.GetService<IRepositoryLogAction>();
                    var efContextBot = scope.ServiceProvider.GetService<IRepositoryBot>();
                    var ListMessage = efContextMessage.UnreadMessages();
                    var ListAction = efContextAction.GetLogUnderRead();
                    /*Работа с собщениями */
                    if (ListMessage.Any())
                    {
                        WorkMessage(efContextBot, efContextMessage);
                    }
                    /*Работа с событиями */
                    if (ListAction.Any())
                    {
                        WorkEvent(efContextBot, efContextAction);
                    }
                }
              //  await Task.Delay(10000);
            }
        }
        private void WorkMessage(IRepositoryBot efContextBot, IRepositoryMessage efContextMessage)
        {
            var ListMessage = efContextMessage.UnreadMessages();
            var ListBot = efContextBot.Bots;
            List<Message> messagesBots = new List<Message>();
            foreach (var mes in ListMessage)
            {
                if (mes.Chat.NameBots == null) { break; }
                string[] contentMes = mes.Content.Trim().Split(" ");
                foreach (var bot in ListBot)
                {
                    string mesBot = "";
                    foreach (var word in contentMes)
                    {
                        mesBot = bot.Move(word.ToUpper());
                        if (!String.IsNullOrEmpty(mesBot))
                        {
                            Message message = new Message(mesBot, bot.NameBot);
                            message.Chat = mes.Chat;
                            messagesBots.Add(message);
                        }
                    }
                }
                mes.Undread = false;
            }
            efContextMessage.AddMessages(messagesBots);
        }
        private void WorkEvent(IRepositoryBot efContextBot, IRepositoryLogAction efContextAction)
        {
            var ListLogsAction = efContextAction.GetLogUnderRead();
            var ListBot = efContextBot.Bots;
            List<Message> messagesBots = new List<Message>();
            foreach (var action in ListLogsAction)
            {
                if (action.Chat.ChatBot.Any())
                {
                    foreach(var bot in ListBot)
                    {

                    }
                }
            }
        }
    }
}
