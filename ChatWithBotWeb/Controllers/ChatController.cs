using ChatWithBotWeb.Infrastructure;
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
using System.Diagnostics;
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
            List<string> ListBotNames;
            if (chat.ChatBot.Any())
            {
                ListBotNames = repositoryBot.BotNames.Except(chat.NameBots).ToList();
            }
            else
            {
                ListBotNames = repositoryBot.BotNames.ToList();
            }
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
        [HttpPost]
        public ActionResult AddMessage(string content,int chatId)
        {
            if (!String.IsNullOrEmpty(content))
            {
                Chat chat = repositoryChat.GetChat(chatId);
                var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
                Message message = new Message(content, user);
                chat.ListMessage.Add(message);
                LogAction logAction = new LogAction(DateTime.Now, EventChat.SendMessage, user);
                chat.LogActions.Add(logAction);
                repositoryChat.UpdateChat(chat);
            }
            else
            {
                TempData["Errors"] = "Нельзя отправить пустое сообщение";
            }
            return RedirectToAction("Index", new { IdChat = chatId });

        }
        [HttpPost]
        public ActionResult DeleteMessage(int messageId, int chatId)
        {
            Chat chat = repositoryChat.GetChat(chatId);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Message message=chat.ListMessage.Find(m=>m.MessageId==messageId);
            if (message.User != null && message.User.Equals(user))
            {
                chat.ListMessage.Remove(message);
                LogAction logAction = new LogAction(DateTime.Now, EventChat.DeleteMessage, user);
                chat.LogActions.Add(logAction);
                repositoryChat.UpdateChat(chat);

                return RedirectToAction("Index", new { IdChat = chatId });
            }
            else
            {
                TempData["Errors"] = "Вы не можете удалить сообщение не принадлежащее вам";
                return RedirectToAction("Index", new { IdChat = chatId });
            }
        }
        [HttpPost]
        public ActionResult AddUserInChat(string UserId , int ChatId)
        {
            User user = repositoryUser.GetUser(UserId);
            var currentUser = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

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
            LogAction logAction = new LogAction(DateTime.Now, EventChat.DeleteMessage, currentUser);
            chat.LogActions.Add(logAction);
            repositoryChat.UpdateChat(chat);
            return RedirectToAction("Index", new { IdChat= ChatId });
            
        }
        [HttpPost]
        public ActionResult AddBot(int ChatId, string NameBot)
        {
            Chat chat = repositoryChat.GetChat(ChatId);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (ModelState.IsValid)
            {
                chat.NameBots.Add(NameBot);
                LogAction logAction = new LogAction(DateTime.Now, EventChat.InviteBot, user);
                chat.LogActions.Add(logAction);
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
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            chat.NameBots.Remove(NameBot);
            LogAction logAction = new LogAction(DateTime.Now, EventChat.DeleteBot, user);
            chat.LogActions.Add(logAction);
            repositoryChat.UpdateChat(chat);
            return RedirectToAction("Index", new { IdChat = ChatId });
        }
        [HttpPost]
        public ActionResult DeleteUserInChat(string UserId, int ChatId)
        {
            User user = repositoryUser.GetUser(UserId);
            Chat chat = repositoryChat.GetChat(ChatId);
            var currentUser = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            chat.Users.Remove(user);
            LogAction logAction = new LogAction(DateTime.Now, EventChat.DeletePerson, currentUser);
            chat.LogActions.Add(logAction);
            repositoryChat.UpdateChat(chat);
            return RedirectToAction("Index", new { IdChat = ChatId });
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
