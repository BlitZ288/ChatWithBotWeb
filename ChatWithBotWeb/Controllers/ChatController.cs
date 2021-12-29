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
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Controllers
{
    public class ChatController : Controller
    {
        private IRepositoryUser repositoryUser;
        private IRepositoryChat repositoryChat;
        private IRepositoryLogUser repositoryLogUser;



        public ChatController(IRepositoryUser Usercontext, IRepositoryChat Chatcontext, IRepositoryLogUser LogUsercontext)
        {
            repositoryUser = Usercontext;
            repositoryChat = Chatcontext;
            repositoryLogUser = LogUsercontext;
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult CreateChat()
        {
            return View("CreateChat");
        }
       
        public ActionResult ShowCard()
        {
            var ListChat = repositoryChat.GetAllChat.OrderBy(c => c.ChatId);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<ChatViewModel> model = new List<ChatViewModel>();
            ViewData["test"] = $"Текущий пользователь {user.Name}";
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
                var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (!chat.Users.Contains(user))
                {
                    chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User=user });
                    chat.Users.Add(user);
                    repositoryChat.UpdateChat(chat);
                }
                var ListUsers = repositoryUser.GetAllUsers.Except(chat.Users).ToList(); //////Подумать 
                ChatUserViewModel model = new ChatUserViewModel()
                {
                    Chat = chat,
                    UsersNotInclude = ListUsers,
                    HistoryChat = GetHistoryChat(chat, user)
                };
                return View("Index", model);
            }
            catch
            {
                return RedirectToAction("ShowCard");
            }
        }

        [HttpPost]
        public ActionResult CreateChat(Chat chat)
        {
            try
            {
                var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
                chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user });
                chat.Users.Add(user);
                repositoryChat.AddChat(chat);
                var ListUsers = repositoryUser.GetAllUsers.Except(chat.Users).ToList();////
                ChatUserViewModel model = new ChatUserViewModel()
                {
                    Chat = chat,
                    UsersNotInclude = ListUsers,
                    HistoryChat = GetHistoryChat(chat, user)
                };
                return View("Index", model);
            }
            catch 
            {
                return View();
            }
        }

        public ActionResult AddMessage(string content,int chatId)
        {
           
            Chat chat = repositoryChat.GetChat(chatId);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (chat.Users.Contains(user))
            {
                Message message = new Message(content, user);
                chat.ListMessage.Add(message);
                //var listMesseg = chat.GetHistoryChat(chat, user);
                repositoryChat.UpdateChat(chat);
                return SelectChat(chat.ChatId);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Вы не являетесь участником чата");
                return null;
            }

        }
        
        [HttpPost]
        public ActionResult AddUserInChat(string UserId , int ChatId)
        {
            User user = repositoryUser.GetUser(UserId);
            Chat chat = repositoryChat.GetChat(ChatId);
            LogsUser logsUser = repositoryLogUser.GetLog(user,chat);
            if (logsUser==null)
            {
                LogsUser logs = new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user };
                chat.ChatLogUsers.Add(logs);
            }
            else
            {
                chat.ChatLogUsers.ElementAt(logsUser.LogsUserId).StopChat = null;
            }
            chat.Users.Add(user);
            repositoryChat.UpdateChat(chat);
            var ListUsers = repositoryUser.GetAllUsers.Except(chat.Users).ToList();
            ChatUserViewModel model = new ChatUserViewModel()
            {
                Chat = chat,
                UsersNotInclude = ListUsers,
                HistoryChat = GetHistoryChat(chat, user)

            };
            return View("Index", model);
        }
        [HttpPost]
        public ActionResult DeleteUserInChat(string UserId, int ChatId)
        {
            User user = repositoryUser.GetUser(UserId);
            Chat chat = repositoryChat.GetChat(ChatId);
            chat.Users.Remove(user);
            repositoryChat.UpdateChat(chat);
            var ListUsers = repositoryUser.GetAllUsers.Except(chat.Users).ToList();///Подумать 
            ChatUserViewModel model = new ChatUserViewModel()
            {
                Chat = chat,
                UsersNotInclude = ListUsers,
                HistoryChat = GetHistoryChat(chat, user)
            };
            return View("Index", model);
        }

        private List<Message> GetHistoryChat(Chat chat, User user)
        {
            List<Message> result = new List<Message>();
            var logUser = repositoryLogUser.GetLog(user, chat);
            try
            {
                if (logUser != null)
                {
                    var mes = chat.ListMessage.Where(m => logUser.StartChat <= m.dateTime);
                    if (logUser.StopChat != null)
                    {
                        mes = chat.ListMessage.Where(m => m.dateTime < logUser.StopChat);
                    }
                    if (mes.Any())
                    {

                        return mes.ToList();
                    }
                }
            }
            catch
            {
                return result;
            }
            return result;
        }
    }
}
