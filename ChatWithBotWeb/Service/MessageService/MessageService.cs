using ChatWithBotWeb.Service.MessageService.Interface;
using Domian;
using Domian.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ChatWithBotWeb.Service.MessageService
{
    public class MessageService : IMessageService
    {
        IUnitOfWorck Database { get; set; }

        public MessageService(IUnitOfWorck unit)
        {
            Database = unit;
        }
        public void CreateMessage(string content, User user)
        {
            Message message = new Message(content, user);
            Database.Message.Create(message);
        }

        public void DeleteMessage(int idMessage)
        {
            Database.Message.Delete(idMessage);
        }
        public IEnumerable<Message> GetAllMessages()
        {
            return Database.Message.GetAll();
        }

        public Message GetMessageById(int idMessage)
        {
            return Database.Message.Get(idMessage);
        }

        public IEnumerable<Message> GetMessagesByChat(int chatId)
        {
            return Database.Message.GetAll().Where(m => m.ChatId == chatId);

        }

        public IEnumerable<Message> HistoryMessagesForChat(int idChat, int idUser)
        {
            var logUser = Database.LogsUsers.GetAll().FirstOrDefault(l => l.ChatId == idChat && l.UserId == idUser);

            if (logUser.StopChat == null)
            {
                return Database.Message.GetAll().Where(m => m.ChatId == idChat && logUser.StartChat <= m.dateTime);
            }
            else
            {
                return Database.Message.GetAll().Where(m => m.ChatId == idChat && logUser.StartChat <= m.dateTime && m.dateTime <= logUser.StopChat);
            }
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
