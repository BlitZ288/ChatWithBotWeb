﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Interface
{
   public interface IRepositoryMessage
    {
        List<Message> GetMessages { get; }
        List<Message> UnreadMessages();
        void AddMessages(List<Message> message);
    }
}
