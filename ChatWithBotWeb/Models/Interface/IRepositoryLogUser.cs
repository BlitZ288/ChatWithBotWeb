using System.Collections.Generic;

namespace ChatWithBotWeb.Models.Interface
{
    public interface IRepositoryLogUser
    {
        List<LogsUser> GetAll();
        LogsUser GetLog(User user, Chat chat);
        void AddLog(LogsUser logsUser);
        void DeleteLog(LogsUser logsUser);
    }
}
