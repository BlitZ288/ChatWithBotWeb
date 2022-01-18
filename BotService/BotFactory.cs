using Coman.InterfaceBots;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BotService
{
    public class BotFactory
    {
        private IEnumerable<IEventBot> listEventBots ;
        private IEnumerable<IMessageBot> listMessageBots;
        private IEnumerable<string> listNameBots; 
        public BotFactory()
        {
            this.listEventBots = Enumerable.Empty<IEventBot>();
            this.listMessageBots = Enumerable.Empty<IMessageBot>();
            this.listNameBots = new List<string>();
        }

        public IEnumerable<IEventBot> ListEventBots => listEventBots;

        public IEnumerable<IMessageBot> ListMessageBots => listMessageBots;

        public IEnumerable<string> ListNameBots => listNameBots;

        public void Init()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\AssemblyBots", "*.dll");
            foreach (string dll in files)
            {
                var assembly = Assembly.LoadFile(dll);
                var types = assembly.GetTypes();
                
                foreach(var type in types)
                {
                    if(type.GetInterface("IEventBot") != null)
                    {
                        IEventBot eventBot=(IEventBot)Activator.CreateInstance(type);
                        listEventBots=listEventBots.Concat(new[] { eventBot });    
                    }
                    if(type.GetInterface("IMessageBot") != null)
                    {
                        IMessageBot eventBot = (IMessageBot)Activator.CreateInstance(type);
                        listMessageBots = listMessageBots.Concat(new[] { eventBot });
                    }
                }
                //listEventBots= (IEnumerable<IEventBot>)listEventBots.Concat(types.Where(t => t.GetInterface("IEventBot") != null).Select(Activator.CreateInstance));
                //this.listEventBots.Union(types.Where(t => t.GetInterface("IEventBot") != null).Select(Activator.CreateInstance));
                //this.listMessageBots.Union(types.Where(t => t.GetInterface("IMessageBot") != null).Select(Activator.CreateInstance));               
                listNameBots = listNameBots.Union(types.Select(t => t.Name));               
            }

        }

    }
}
