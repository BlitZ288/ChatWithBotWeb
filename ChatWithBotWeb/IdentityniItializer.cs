using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb
{
    public class IdentityniItializer : Controller
    {
        private static IRepositoryUser repositoryUser;
        private static RoleManager<IdentityRole> _roleManager;
        private static UserManager<User> _userManager;
     
        private const string adminPassword = "1234";

        public IdentityniItializer(IRepositoryUser Usercontext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            repositoryUser = Usercontext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public static async Task EnsurePopularity(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IRepositoryUser Usercontext)
        {
            User admin = await userManager.FindByNameAsync("Admin");
            if (admin == null)
            {
                admin = new User("Admin", adminPassword);
                admin.UserName = "Admin";
                var result= await userManager.CreateAsync(admin, adminPassword);

                if (await roleManager.FindByNameAsync("Manager") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Manager"));
                }
                await userManager.AddToRoleAsync(admin, "Manager");
                Usercontext.AddUser(admin);
            }

        }
    }
}

