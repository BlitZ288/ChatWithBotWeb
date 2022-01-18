﻿using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string NameChat { get; set; }
        public List<User> Users { get; set; }
        public IEnumerable<string> NameBots { get; set; }
    }
}
