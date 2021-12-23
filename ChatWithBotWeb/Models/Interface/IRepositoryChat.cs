using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Interface
{
    public interface IRepositoryChat
    {
        List<Chat> GetAllChat { get; }
        void AddChat(Chat chat);
        Chat GetChat(int indexChat);
        void DeleteChat(Chat chat);

    }
}
