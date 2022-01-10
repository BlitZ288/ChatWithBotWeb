using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Interface
{
    interface IRepositoryLogAction
    {
        List<LogAction> GetAll();
        List<LogAction> GetLogUnderRead();
        void AddLog(LogAction logsAction);
        void DeleteLog(LogAction logsAction);
    }
}
