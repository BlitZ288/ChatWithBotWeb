using System.Collections.Generic;

namespace ChatWithBotWeb.Models.Interface
{
    public interface IRepositoryMessage
    {
        List<Message> GetMessages { get; }
        List<Message> UnreadMessages();
        void AddMessages(List<Message> message);
    }
}
