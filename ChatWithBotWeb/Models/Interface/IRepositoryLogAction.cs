using System.Collections.Generic;

namespace ChatWithBotWeb.Models.Interface
{
    interface IRepositoryLogAction
    {
        List<LogAction> GetAll();
        List<LogAction> GetLogUnderRead();
        void Update(List<LogAction> logsAction);
        void Update(LogAction logsAction);
        void AddLog(LogAction logsAction);
        void DeleteLog(LogAction logsAction);
    }
}
