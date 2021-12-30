using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using ChatWithBotWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]

    public class ChatController : Controller
    {
        private IRepositoryUser repositoryUser;
        private IRepositoryChat repositoryChat;
        private IRepositoryLogUser repositoryLogUser;
        private IRepositoryBot repositoryBot;



        public ChatController(IRepositoryUser Usercontext, IRepositoryChat Chatcontext, IRepositoryLogUser LogUsercontext, IRepositoryBot Botcontext)
        {
            repositoryUser = Usercontext;
            repositoryChat = Chatcontext;
            repositoryLogUser = LogUsercontext;
            repositoryBot = Botcontext;
        }
        public ActionResult Index(int IdChat)
        {
        
            Chat chat = repositoryChat.GetChat(IdChat);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewData["NameUser"] = user.Name;
            if (!chat.Users.Contains(user))
            {
                chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user });
                chat.Users.Add(user);
                repositoryChat.UpdateChat(chat);
            }
            var ListUsers = repositoryUser.GetAllUsers.Except(chat.Users).ToList(); //////Подумать 
            var ListBotNames =  repositoryBot.BotNames.Except(chat.NameBots).ToList();
            ChatUserViewModel model = new ChatUserViewModel()
            {
                Chat = chat,
                UsersNotInclude = ListUsers,
                HistoryChat = GetHistoryChat(chat, user),
                CurrentUser = user,
                NameBot = ListBotNames
            };
            return View(model);
        }

        public ActionResult CreateChat()
        {
            return View("CreateChat");
        }
       
        public ActionResult ShowCard()
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
            if (ModelState.IsValid)
            {
              var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
              chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user });
              chat.Users.Add(user);
              repositoryChat.AddChat(chat);
              return RedirectToAction("Index", new { IdChat = chat.ChatId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Имя чата объязательно");
                return View();
            }
        }

        public ActionResult AddMessage(string content,int chatId)
        {

            if (!String.IsNullOrEmpty(content))
            {
                Chat chat = repositoryChat.GetChat(chatId);
                var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
                Message message = new Message(content, user);
                chat.ListMessage.Add(message);
                repositoryChat.UpdateChat(chat);
           

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Нельзя отправить пустое сообщение");
            }
            return RedirectToAction("Index", new { IdChat = chatId });

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
            return RedirectToAction("Index", new { IdChat= ChatId });
            
        }
        [HttpPost]
        public ActionResult AddBot(int ChatId, string NameBot)
        {
            Chat chat = repositoryChat.GetChat(ChatId);
            if (ModelState.IsValid)
            {
                
                chat.NameBots.Add(NameBot);
                repositoryChat.UpdateChat(chat);
                return RedirectToAction("Index", new { IdChat = ChatId });
            }
            else
            {
                return RedirectToAction("Index", new { IdChat = ChatId });

            }
        }
        [HttpPost]
        public ActionResult DeleteBot(int ChatId, string NameBot)
        {
            Chat chat = repositoryChat.GetChat(ChatId);
            chat.NameBots.Remove(NameBot);
            repositoryChat.UpdateChat(chat);
            return RedirectToAction("Index", new { IdChat = ChatId });
        }
        [HttpPost]
        public ActionResult DeleteUserInChat(string UserId, int ChatId)
        {
            User user = repositoryUser.GetUser(UserId);
            Chat chat = repositoryChat.GetChat(ChatId);
            chat.Users.Remove(user);
            repositoryChat.UpdateChat(chat);
            return RedirectToAction("Index", new { IdChat = ChatId });
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult DeleteChat(int ChatId)
        {
            Chat chat = repositoryChat.GetChat(ChatId);
            repositoryChat.DeleteChat(chat);
            return RedirectToAction("ShowCard");
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
