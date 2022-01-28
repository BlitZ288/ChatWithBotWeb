using Domian.Entities;
using System.Collections.Generic;

namespace ChatWithBotWeb.Service.ChatService.Interface
{
    public interface IChatService
    {
        IEnumerable<Chat> GetAllChats();
        Chat GetChatById(int idChat);

        void CreateChat(string name);
        void DeleteChat(int idChat);

        void UpdateChat();
        void Dispose();
    }
}
