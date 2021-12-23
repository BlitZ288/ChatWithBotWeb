using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Db;
using ChatWithBotWeb.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRepositoryUser conextUser;
        private IRepositoryChat conextChat;
        public HomeController(ILogger<HomeController> logger, IRepositoryUser context, IRepositoryChat repositoryChat)
        {
            _logger = logger;
            conextUser = context;
            conextChat = repositoryChat;
        }

        public IActionResult Index()
        {
            
          
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
