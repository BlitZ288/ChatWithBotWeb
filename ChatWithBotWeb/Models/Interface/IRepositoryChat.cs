using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Interface
{
    public interface IRepositoryChat
    {
        List<Chat> GetAllChat { get; }
        void AddChat(Chat chat);
        Chat GetChat(int indexChat);
        void DeleteChat(Chat chat);
        Chat DeleteUserChat(Chat chat, User user);
        Chat AddUserChat(Chat chat, User user);
        void UpdateChat(Chat chat);
        Task UpdateChatAsync(Chat chat);

    }
}
