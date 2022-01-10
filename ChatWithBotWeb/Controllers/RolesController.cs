using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> RoleManager;
        UserManager<User> UserManager;
        public RolesController(RoleManager<IdentityRole> roleManager , UserManager<User> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }
        public IActionResult Index()
        {
            return View(RoleManager.Roles.ToList());
        }
        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                IdentityResult result = await RoleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        public IActionResult UserList() => View(UserManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            User user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await UserManager.GetRolesAsync(user);
                var allRoles = RoleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserRoles = userRoles,
                    AllRoles = allRoles,
                    UserName = user.Name
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await UserManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = RoleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await UserManager.AddToRolesAsync(user, addedRoles);

                await UserManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}
