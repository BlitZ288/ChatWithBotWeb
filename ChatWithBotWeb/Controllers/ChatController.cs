using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ChatController( IRepositoryUser Usercontext, IRepositoryChat Chatcontext)
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
            var ListChat = repositoryChat.GetAllChat;
           List <ViewChat> model = new List<ViewChat>();
           
            foreach(var chat in ListChat)
            {
                model.Add(new ViewChat() 
                { 
                    Id=chat.ChatId,
                    Bots=chat.ChatBot,
                    NameChat=chat.Name,
                    Users=chat.Users
                });

            }
            return View("SelectChat", model);
        }
        [HttpPost]
        public void SelectChat(int id)
        {
            int a = id;
            int b = 2; 
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

        // GET: ChatController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChatController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChatController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChatController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChatController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChatController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
