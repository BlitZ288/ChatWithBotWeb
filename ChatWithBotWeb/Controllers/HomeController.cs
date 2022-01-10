using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Db;
using ChatWithBotWeb.Models.Interface;
using ChatWithBotWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IRepositoryUser repositoryUser;
        private IRepositoryChat repositoryChat;
        public HomeController(IRepositoryUser Usercontext, IRepositoryChat Chatcontext)
        {
            repositoryUser = Usercontext;
            repositoryChat = Chatcontext;
        }
        public ActionResult Index()
        {
            var ListChat = repositoryChat.GetAllChat.OrderBy(c => c.ChatId);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewData["NameUser"] = user.Name;
            List<ChatViewModel> model = new List<ChatViewModel>();
            foreach (var chat in ListChat)
            {
                model.Add(new ChatViewModel()
                {
                    Id = chat.ChatId,
                    Bots = chat.ChatBot,
                    NameChat = chat.Name,
                    Users = chat.Users,
                });
            }

            return View("Index", model);
        }
        public ActionResult CreateChat()
        {
            return View("CreateChat");
        }
        [HttpPost]
        public ActionResult CreateChat(Chat chat)
        {
            
            if (ModelState.IsValid)
            {
                var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
                chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user });
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                LogAction logAction = new LogAction(DateTime.Now, sf.GetMethod().Name, user);
                chat.LogActions.Add(logAction);
                chat.Users.Add(user);
                repositoryChat.AddChat(chat);
                return RedirectToAction("Index","Chat", new { IdChat = chat.ChatId });
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult DeleteChat(int IdChat)
        {
            Chat chat = repositoryChat.GetChat(IdChat);
            repositoryChat.DeleteChat(chat);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult SelectChat(int IdChat)
        //{
        //    try
        //    {
        //        Chat chat = repositoryChat.GetChat(IdChat);
        //        var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
               
        //        if (!chat.Users.Contains(user))
        //        {
        //            chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user });
        //            chat.Users.Add(user);
        //            repositoryChat.UpdateChat(chat);
        //        }
        //        return RedirectToAction("Index", "Chat", new { IdChat = chat.ChatId });
        //    }
        //    catch
        //    {
        //        return RedirectToAction("ShowCard");
        //    }
        //}
    }
}
