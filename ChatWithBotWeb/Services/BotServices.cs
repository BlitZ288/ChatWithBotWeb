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
                    var efContext = scope.ServiceProvider.GetService<IRepositoryMessage>();
                    
                    var ListMessage = efContext.UnreadMessages();
                    if (ListMessage.Any())
                    {
                        var efContextBot = scope.ServiceProvider.GetService<IRepositoryBot>();
                        var ListBot = efContextBot.Bots;
                        List<Message> messagesBots = new List<Message>();
                        foreach(var mes in ListMessage)
                        {
                            string[] contentMes = mes.Content.Trim().Split(" ");
                            foreach(var bot in ListBot)
                            {
                                string mesBot = "";
                                foreach(var word in contentMes)
                                {
                                    mesBot= bot.Move(word.ToUpper());
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
                        efContext.AddMessages(messagesBots);
                    }

                }
                await Task.Delay(10000);
            }
        }
    }
}
