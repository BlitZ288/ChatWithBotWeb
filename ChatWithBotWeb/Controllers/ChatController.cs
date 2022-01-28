using ChatWithBotWeb.Models.ViewModels;
using ChatWithBotWeb.Service.ChatService.Interface;
using ChatWithBotWeb.Service.MessageService.Interface;
using ChatWithBotWeb.Service.UserService.Interface;
using Coman;
using Domian.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ChatWithBotWeb.Controllers
{
    public class ChatController : Controller
    {
        private IChatService chatService;
        private IMessageService messageService;
        private IUserService userService;

        public ChatController(IChatService chatService, IMessageService messageService, IUserService userService)
        {
            this.chatService = chatService;
            this.messageService = messageService;
            this.userService = userService;
        }
        [Authorize]
        public IActionResult Index(int idChat)
        {
            var chat = chatService.GetChatById(idChat);
            var user = userService.GetUserByName(ClaimTypes.Name);

            ViewData["NameUser"] = user.Name;

            if (!chat.Users.Contains(user))
            {
                chat.Users.Add(user);
                chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user });

                chat.LogActions.Add(new LogAction(DateTime.Now, EventChat.JoinChat, user));

                chatService.UpdateChat();
            }

            var availableUsers = userService.GetAllUser().Except(chat.Users);

            var chatBots = userService.GetBotsNameByIdChat(idChat);
            IEnumerable<string> availableBots;

            if (chatBots.Any())
            {
                availableBots = userService.GetAllBotsName().Except(chatBots);
            }
            else
            {
                availableBots = userService.GetAllBotsName();
            }

            ChatUserViewModel model = new ChatUserViewModel()
            {
                ChatId = idChat,
                UsersNotInclude = availableUsers,
                ChatUsers = chat.Users,
                HistoryChat = messageService.HistoryMessagesForChat(idChat, user.IdUser),
                CurrentUser = user,
                AvailableBots = availableBots,
                ChatBots = chatBots
            };

            if (TempData["refresh"] != null)
            {
                HttpContext.Response.Headers.Add("refresh", "15; url=" + Url.Action("Index", new
                {
                    idChat = idChat
                }));
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMessageInChat(string content, int chatId)
        {
            if (!String.IsNullOrEmpty(content))
            {
                var user = userService.GetUserByName(ClaimTypes.Name);
                var chat = chatService.GetChatById(chatId);

                Message message = new Message(content, user);
                chat.ListMessage.Add(message);

                chatService.UpdateChat();

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
            var message = messageService.GetMessageById(messageId);
            var user = userService.GetUserByName(ClaimTypes.Name);
            if (message.User.IdUser == user.IdUser)
            {
                messageService.DeleteMessage(messageId);
                chatService.UpdateChat();

                return RedirectToAction("Index", new { idChat = chatId });
            }
            else
            {
                TempData["Errors"] = "Вы не можете удалить сообщение не принадлежащее вам";
                return RedirectToAction("Index", new { idChat = chatId });
            }
        }
        [HttpPost]
        public ActionResult AddUserInChat(int userId, int chatId)
        {
            var user = userService.GetUser(userId);
            var currentUser = userService.GetUserByName(ClaimTypes.Name);

            var chat = chatService.GetChatById(chatId);

            var logUser = chat.ChatLogUsers.FirstOrDefault(l => l.UserId == userId);
            if (logUser == null)
            {
                LogsUser logs = new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user };
                chat.ChatLogUsers.Add(logs);
            }
            else
            {
                logUser.StopChat = null;
            }
            chat.Users.Add(user);

            LogAction logAction = new LogAction(DateTime.Now, EventChat.JoinChat, currentUser);
            chat.LogActions.Add(logAction);

            chatService.UpdateChat();

            //Task.Run(() => BotEventMove(logAction.Content, chatId, chat.NameBots));

            return RedirectToAction("Index", new { idChat = chatId });
        }

        [HttpPost]
        public ActionResult DeleteUserInChat(int userId, int chatId)
        {
            var user = userService.GetUser(userId);
            var chat = chatService.GetChatById(chatId);

            var logUser = chat.ChatLogUsers.FirstOrDefault(l => l.UserId == userId);
            logUser.StopChat = DateTime.Now;

            if (chat.Users.Count == 0)
            {
                chatService.DeleteChat(chatId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                chatService.UpdateChat();
                return RedirectToAction("Index", new { idChat = chatId });
            }

        }
    }
}
