using BotService;
using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using ChatWithBotWeb.Models.ViewModels;
using Coman;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly BotsManager managerBots;
        private IServiceScopeFactory _serviceScopeFactory;

        public ChatController(IRepositoryUser usercontext, IRepositoryChat chatcontext, IRepositoryLogUser logUsercontext, IServiceScopeFactory serviceScopeFactory, BotsManager managerBots)
        {
            this.repositoryUser = usercontext;
            this.repositoryChat = chatcontext;
            this.repositoryLogUser = logUsercontext;
            this.managerBots = managerBots;
            this._serviceScopeFactory = serviceScopeFactory;
        }
        [Authorize]
        public ActionResult Index(int idChat)
        {
            Chat chat = repositoryChat.GetChat(idChat);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewData["NameUser"] = user.Name;

            if (!chat.Users.Contains(user))
            {
                chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user });
                chat.Users.Add(user);

                LogAction logAction = new LogAction(DateTime.Now, EventChat.JoinChat, user);
                chat.LogActions.Add(logAction);
                repositoryChat.UpdateChat(chat);
            }
            var listUsers = repositoryUser.GetAllUsers.Except(chat.Users).ToList();

            List<string> listBotNames;
            if (chat.NameBots.Any())
            {
                listBotNames = managerBots.GetAllNameBots().Except(chat.NameBots).ToList();
            }
            else
            {
                listBotNames = managerBots.GetAllNameBots().ToList();
            }
            ChatUserViewModel model = new ChatUserViewModel()
            {
                Chat = chat,
                UsersNotInclude = listUsers,
                HistoryChat = GetHistoryChat(chat, user),
                CurrentUser = user,
                ListNameBot = listBotNames
            };

            if (TempData["refresh"] != null)
            {
                HttpContext.Response.Headers.Add("refresh", "15; url=" + Url.Action("Index", new { idChat = idChat }));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMessageInChat(string content, int chatId)
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

                Task.Run(() => BotMessageMove(content, chatId, chat.NameBots));


                TempData["refresh"] = true;
            }
            else
            {
                TempData["Errors"] = "Нельзя отправить пустое сообщение";
            }
            return RedirectToAction("Index", new { idChat = chatId });
        }

        [HttpPost]
        public ActionResult DeleteMessage(int messageId, int chatId)
        {
            Chat chat = repositoryChat.GetChat(chatId);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Message message = chat.ListMessage.Find(m => m.MessageId == messageId);

            if (message.User != null && message.User.Equals(user))
            {
                chat.ListMessage.Remove(message);
                LogAction logAction = new LogAction(DateTime.Now, EventChat.DeleteMessage, user);

                chat.LogActions.Add(logAction);
                repositoryChat.UpdateChat(chat);

                return RedirectToAction("Index", new { idChat = chatId });
            }
            else
            {
                TempData["Errors"] = "Вы не можете удалить сообщение не принадлежащее вам";
                return RedirectToAction("Index", new { idChat = chatId });
            }
        }

        [HttpPost]
        public ActionResult AddUserInChat(string userId, int chatId)
        {
            User user = repositoryUser.GetUser(userId);
            var currentUser = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Chat chat = repositoryChat.GetChat(chatId);
            LogsUser logsUser = repositoryLogUser.GetLog(user, chat);
            if (logsUser == null)
            {
                LogsUser logs = new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user };
                chat.ChatLogUsers.Add(logs);
            }
            else
            {
                logsUser.StopChat = null;
            }
            chat.Users.Add(user);

            LogAction logAction = new LogAction(DateTime.Now, EventChat.JoinChat, currentUser);
            chat.LogActions.Add(logAction);

            repositoryChat.UpdateChat(chat);

            Task.Run(() => BotEventMove(logAction.Content, chatId, chat.NameBots));

            return RedirectToAction("Index", new { idChat = chatId });
        }

        [HttpPost]
        public ActionResult AddBotInChat(int chatId, string nameBot)
        {
            Chat chat = repositoryChat.GetChat(chatId);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (ModelState.IsValid)
            {
                chat.NameBots.Add(nameBot);
                LogAction logAction = new LogAction(DateTime.Now, EventChat.InviteBot, user);
                chat.LogActions.Add(logAction);
                repositoryChat.UpdateChat(chat);
            }
            return RedirectToAction("Index", new { idChat = chatId });
        }
        [HttpPost]
        public ActionResult DeleteBot(int chatId, string nameBot)
        {
            Chat chat = repositoryChat.GetChat(chatId);
            var user = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            chat.NameBots.Remove(nameBot);

            LogAction logAction = new LogAction(DateTime.Now, EventChat.DeleteBot, user);
            chat.LogActions.Add(logAction);
            repositoryChat.UpdateChat(chat);

            return RedirectToAction("Index", new { idChat = chatId });
        }
        [HttpPost]
        public ActionResult DeleteUserInChat(string userId, int chatId)
        {
            User user = repositoryUser.GetUser(userId);
            Chat chat = repositoryChat.GetChat(chatId);
            var currentUser = repositoryUser.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            chat.Users.Remove(user);

            LogsUser logsUser = repositoryLogUser.GetLog(user, chat);
            logsUser.StopChat = DateTime.Now;
            LogAction logAction = new LogAction(DateTime.Now, EventChat.DeletePerson, currentUser);
            chat.LogActions.Add(logAction);

            if (chat.Users.Count == 0)
            {
                repositoryChat.DeleteChat(chat);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                repositoryChat.UpdateChat(chat);
                return RedirectToAction("Index", new { idChat = chatId });
            }

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
                        mes = chat.ListMessage.Where(m => m.dateTime <= logUser.StopChat);
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

        private void BotMessageMove(string content, int chatId, IEnumerable<string> nameBots)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IRepositoryChat>();

                var botAnswer = managerBots.GetMessage(content, nameBots);

                var chat = repository.GetChat(chatId);

                if (botAnswer != null)
                {
                    Message botMessage = new Message(botAnswer.Content, botAnswer.OwnerAnswer);
                    botMessage.Chat = chat;
                    chat.ListMessage.Add(botMessage);

                    repository.UpdateChat(chat);
                }
            }

        }
        private void BotEventMove(EventChat eventChat, int chatId, IEnumerable<string> nameBots)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IRepositoryChat>();

                var botAnswer = managerBots.GetEventMessage(eventChat, nameBots);

                var chat = repository.GetChat(chatId);

                if (botAnswer != null)
                {

                    Message botMessage = new Message(botAnswer.Content, botAnswer.OwnerAnswer);
                    botMessage.Chat = chat;
                    chat.ListMessage.Add(botMessage);

                    repository.UpdateChat(chat);
                }
            }
        }
    }
}
