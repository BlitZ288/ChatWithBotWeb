using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using ChatWithBotWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IRepositoryUser repositoryUser;
        public AccountController(UserManager<User> userMgr, SignInManager<User> signMgr, IRepositoryUser Usercontext, RoleManager<IdentityRole> roleManager)
        {
            userManager = userMgr;
            signInManager = signMgr;
            repositoryUser = Usercontext;
            IdentityniItializer.EnsurePopularity(userMgr, roleManager, Usercontext).Wait();
        }
        [AllowAnonymous]
        public ViewResult Login(string returnUrl = null)
        {
            LoginModel loginModel = new LoginModel()
            {
                ReturnUrl = returnUrl
            };
            return View(loginModel);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, true, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(loginModel.ReturnUrl) && Url.IsLocalUrl(loginModel.ReturnUrl))
                        {
                            return Redirect(loginModel.ReturnUrl);
                        }
                        else
                        {
                            return Redirect("Home/Index");
                        }

                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password ");
            return View(loginModel);
        }
        public async Task<IActionResult> Logut()
        {
            // удаляем аутентификационные куки
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User(loginModel.Name, loginModel.Password);
                user.UserName = loginModel.Name;
                var result = await userManager.CreateAsync(user, loginModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    repositoryUser.AddUser(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }
    }
}
