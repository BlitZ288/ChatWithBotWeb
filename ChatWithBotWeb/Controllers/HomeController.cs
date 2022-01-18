using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using Coman;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ChatWithBotWeb.Controllers
{
    
    public class HomeController : Controller
    {
        private IRepositoryUser repositoryUser;
        private IRepositoryChat repositoryChat;
        public HomeController(IRepositoryUser usercontext, IRepositoryChat chatcontext)
        {
            repositoryUser = usercontext;
            repositoryChat = chatcontext;
        }
        [Authorize]
        public ActionResult Index()
        {
            var listChat = repositoryChat.GetAllChat.OrderBy(c => c.ChatId);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ViewData["NameUser"] = user.Name;

            List<ChatViewModel> model = new List<ChatViewModel>();
            foreach (var chat in listChat)
            {
                model.Add(new ChatViewModel()
                {
                    Id = chat.ChatId,
                    NameBots = chat.NameBots,
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

                LogAction logAction = new LogAction(DateTime.Now,EventChat.CreateChat, user);
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
        public ActionResult DeleteChat(int idChat)
        {
            Chat chat = repositoryChat.GetChat(idChat);
            repositoryChat.DeleteChat(chat);
            return RedirectToAction("Index");
        }

    }
}
