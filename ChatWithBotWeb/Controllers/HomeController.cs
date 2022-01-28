using ChatWithBotWeb.Models.ViewModels;
using ChatWithBotWeb.Service.ChatService.Interface;
using ChatWithBotWeb.Service.UserService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ChatWithBotWeb.Controllers
{

    public class HomeController : Controller
    {
        private IChatService chatService;
        private IUserService userService;
        public HomeController(IChatService chatService, IUserService userService)
        {
            //repositoryUser = usercontext;
            //repositoryChat = chatcontext;
            this.chatService = chatService;
            this.userService = userService;
        }
        [Authorize]
        public ActionResult Index()
        {
            var listChat = chatService.GetAllChats().OrderBy(c => c.ChatId);
            var user = userService.GetUserByName(ClaimTypes.Name);

            ViewData["NameUser"] = user.Name;

            List<ChatViewModel> model = new List<ChatViewModel>();
            foreach (var chat in listChat)
            {
                model.Add(new ChatViewModel()
                {
                    Id = chat.ChatId,
                    NameBots = userService.GetBotsNameByIdChat(chat.ChatId),
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
        public ActionResult CreateChat(int id)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //    chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user });

            //    LogAction logAction = new LogAction(DateTime.Now, EventChat.CreateChat, user);
            //    chat.LogActions.Add(logAction);
            //    chat.Users.Add(user);

            //    repositoryChat.AddChat(chat);
            //    return RedirectToAction("Index", "Chat", new { IdChat = chat.ChatId });
            //}
            //else
            //{

            //}
            return View();
        }
        [HttpPost]
        public ActionResult DeleteChat(int idChat)
        {
            //Chat chat = repositoryChat.GetChat(idChat);
            //repositoryChat.DeleteChat(chat);
            return RedirectToAction("Index");
        }

    }
}
