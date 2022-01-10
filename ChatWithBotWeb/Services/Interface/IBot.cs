﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Interface
{
    public interface IBot<T>
    {
       
        public string NameBot { get; set; }
        /// <summary>
        /// Действие бота
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public  string Move(T command);
      
        public StringBuilder GetAllCommand();
       
    }
}
