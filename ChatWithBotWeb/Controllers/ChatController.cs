using ChatWithBotWeb.Infrastructure;
using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using ChatWithBotWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<User> _userManager;


        public ChatController(IRepositoryUser Usercontext, IRepositoryChat Chatcontext, UserManager<User> userManager)
        {
            repositoryUser = Usercontext;
            repositoryChat = Chatcontext;
            _userManager = userManager; 

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
        public async Task<ActionResult> SelectChatAsync(int IdChat)
        {
            try
            {
                Chat chat = repositoryChat.GetChat(IdChat);
                User user = await _userManager.GetUserAsync(User);
                User user1 = repositoryUser.GetUser(user.Id);
                if (!chat.Users.Contains(user1))
                {
                    chat.Users.Add(user1);
                    repositoryChat.UpdateChat(chat);
                }
                var ListUsers = repositoryUser.GetAllUsers.Except(chat.Users).ToList();
                ChatUserViewModel model = new ChatUserViewModel()
                {
                    Chat = chat,
                    UsersNotInclude = ListUsers
                };
                HttpContext.Session.SetJson("CurrentChat", chat);
                return View("Index", model);
            }
            catch
            {
                return RedirectToAction("SelectChat");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateChatAsync(Chat chat)
        {/*убрать userManger все мнужно через один context*/
            try
            {
                User t = await _userManager.GetUserAsync(User);
                chat.Users.Add(t);
                repositoryChat.AddChat(chat);
                var ListUsers = repositoryUser.GetAllUsers.Except(chat.Users).ToList();
                ChatUserViewModel model = new ChatUserViewModel()
                {
                    Chat = chat,
                    UsersNotInclude = ListUsers
                };
                return View("Index", model);
            }
            catch(Exception e )
            {

                return View();
            }
        }

        public PartialViewResult AddMessage(string content)
        {
            Message message = new Message(content, new User());
            return null;
        }
        
        [HttpPost]
        public ActionResult AddUserInChat(string UserId)
        {
            User user = repositoryUser.GetUser(UserId);
            Chat chat = HttpContext.Session.GetJson<Chat>("CurrentChat");
            chat.Users.Add(user);
            repositoryChat.UpdateChat(chat);
            return RedirectToAction("SelectChatAsync", chat.ChatId);
        }
        [HttpPost]
        public ActionResult DeleteUserInChat(string UserId)
        {
            User user = repositoryUser.GetUser(UserId);
            Chat chat = HttpContext.Session.GetJson<Chat>("CurrentChat");
            chat.Users.Remove(user);
            repositoryChat.UpdateChat(chat);
            return RedirectToAction("SelectChatAsync", chat.ChatId);
        }

    }
}
