using ChatWithBotWeb.Infrastructure;
using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using ChatWithBotWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Controllers
{
    public class ChatController : Controller
    {
        private IRepositoryUser repositoryUser;
        private IRepositoryChat repositoryChat;
        public ChatController(IRepositoryUser Usercontext, IRepositoryChat Chatcontext)
        {
            repositoryUser = Usercontext;
            repositoryChat = Chatcontext;
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult CreateChat()
        {
            return View("CreateChat");
        }
        public ActionResult SelectChat()
        {
            var ListChat = repositoryChat.GetAllChat.OrderBy(c => c.ChatId);
            List<ChatViewModel> model = new List<ChatViewModel>();

            foreach (var chat in ListChat)
            {
                model.Add(new ChatViewModel()
                {
                    Id = chat.ChatId,
                    Bots = chat.ChatBot,
                    NameChat = chat.Name,
                    Users = chat.Users
                });

            }
            return View("SelectChat", model);
        }
        [HttpPost]
        public ActionResult SelectChat(int IdChat)
        {
            try
            {
                Chat chat = repositoryChat.GetChat(IdChat);
                var ListUsers = repositoryUser.GetAllUsers.Except(chat.Users).ToList();
                ChatUserViewModel model = new ChatUserViewModel()
                {
                    Chat = chat,
                    UsersNotInclude = ListUsers
                };
                SaveChat(chat);
                return View("Index", model);
            }
            catch
            {
                return RedirectToAction("SelectChat");
            }

        }

        [HttpPost]
        public ActionResult CreateChat(Chat chat)
        {
            try
            {
                repositoryChat.AddChat(chat);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public PartialViewResult AddMessage(string content)
        {
            Message message = new Message(content, new User());
            return null;
        }
        private void SaveChat(Chat chat)
        {
            HttpContext.Session.SetJson("CurrentChat", chat);
        }
        private Chat GetChat()
        {
            Chat chat = HttpContext.Session.GetJson<Chat>("CurrentChat");
            return chat;
        }
    }
}
