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
    public class BotsRepository:IBotsRepository
    {
        //private IEnumerable<IEventBot> listEventBots ;
        //private IEnumerable<IMessageBot> listMessageBots;
        //private IEnumerable<string> listNameBots; 
        public BotsRepository()
        {
           // this.listEventBots = Enumerable.Empty<IEventBot>();
           // this.listMessageBots = Enumerable.Empty<IMessageBot>();
            //this.listNameBots = new List<string>();
        }

       

        public IEnumerable<IBot> GetAllBots()
        {
            var bots = new List<IBot>();

            var fileNames = GetAllAssemblyFilesNameFrom("AssemblyBots");
            foreach(var fileName in fileNames)
            {
                //if (сборка содержить тип ){
                //  Bots.add(Activator.CreateInstance(type))
                //
                //}

               /// foreach (var type in types)
            //    {
            //        if (type.GetInterface("IEventBot") != null)
            //        {
            //            IEventBot eventBot = (IEventBot)Activator.CreateInstance(type);
            //            listEventBots = listEventBots.Concat(new[] { eventBot });
            //        }
            //        if (type.GetInterface("IMessageBot") != null)
            //        {
            //            IMessageBot eventBot = (IMessageBot)Activator.CreateInstance(type);
            //            listMessageBots = listMessageBots.Concat(new[] { eventBot });
            //        }
            //    }
            }

            return bots;

            //var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\AssemblyBots", "*.dll");
            //foreach (string dll in files)
            //{
            //    var assembly = Assembly.LoadFile(dll);
            //    var types = assembly.GetTypes();

            //    foreach (var type in types)
            //    {
            //        if (type.GetInterface("IEventBot") != null)
            //        {
            //            IEventBot eventBot = (IEventBot)Activator.CreateInstance(type);
            //            listEventBots = listEventBots.Concat(new[] { eventBot });
            //        }
            //        if (type.GetInterface("IMessageBot") != null)
            //        {
            //            IMessageBot eventBot = (IMessageBot)Activator.CreateInstance(type);
            //            listMessageBots = listMessageBots.Concat(new[] { eventBot });
            //        }
            //    }
            //    //listEventBots= (IEnumerable<IEventBot>)listEventBots.Concat(types.Where(t => t.GetInterface("IEventBot") != null).Select(Activator.CreateInstance));
            //    //this.listEventBots.Union(types.Where(t => t.GetInterface("IEventBot") != null).Select(Activator.CreateInstance));
            //    //this.listMessageBots.Union(types.Where(t => t.GetInterface("IMessageBot") != null).Select(Activator.CreateInstance));               
            //    listNameBots = listNameBots.Union(types.Select(t => t.Name));
            //}
        }
        private bool IsTypeExist(Type type)
        {
            
        }

        private string GetExecutionAssemblyFolder()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

        /// <summary>
        /// Получить название всех файлов с расширением .dll
        /// </summary>
        /// <param name="directory">Целевая папка</param>
        /// <returns></returns>
        private IEnumerable<string> GetAllAssemblyFilesNameFrom(string directory)
        {

            return  Directory.GetFiles(Path.Combine(GetExecutionAssemblyFolder(), directory) , "*.dll");

        }
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
