using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BotWebSite
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("https://localhost:44335/Chat/AddMessage/");
            listener.Start();
            while (!stoppingToken.IsCancellationRequested)
            {
                while (true)
                {
                    HttpListenerContext context = await listener.GetContextAsync();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;
                    int a = 2;
                }

                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}
