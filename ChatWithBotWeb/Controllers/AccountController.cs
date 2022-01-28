using ChatWithBotWeb.Models.ViewModels;
using ChatWithBotWeb.Service.UserService.Interface;
using Domian.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IUserService userService;
        public AccountController(UserManager<User> userMgr, SignInManager<User> signMgr, IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            userManager = userMgr;
            signInManager = signMgr;
            this.userService = userService;
            //IdentityniItializer.EnsurePopularity(userMgr, roleManager, Usercontext).Wait();
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
                User user = new User(loginModel.Name);
                user.UserName = loginModel.Name;
                var result = await userManager.CreateAsync(user, loginModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    //repositoryUser.AddUser(user);
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
