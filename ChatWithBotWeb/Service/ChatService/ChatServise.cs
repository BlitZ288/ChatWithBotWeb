using ChatWithBotWeb.Service.ChatService.Interface;
using Domian;
using Domian.Entities;
using System.Collections.Generic;

namespace ChatWithBotWeb.Service.ChatService
{
    public class ChatServise : IChatService
    {
        IUnitOfWorck Database { get; set; }

        public ChatServise(IUnitOfWorck unit)
        {
            Database = unit;
        }

        public void CreateChat(string name)
        {
            Chat chat = new Chat(name);
            Database.Chats.Create(chat);
        }

        public void DeleteChat(int idChat)
        {
            Database.Chats.Delete(idChat);
        }

        public IEnumerable<Chat> GetAllChats()
        {
            return Database.Chats.GetAll();
        }

        public Chat GetChatById(int idChat)
        {
            return Database.Chats.Get(idChat);
        }
        public void UpdateChat()
        {
            Database.Save();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
