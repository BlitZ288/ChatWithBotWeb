using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb
{
    public class RoleInitializer
    {
        private static IRepositoryUser repositoryUser;

        public RoleInitializer(IRepositoryUser Usercontext)
        {
            repositoryUser = Usercontext;
        }
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminLogin = "admin";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await userManager.FindByNameAsync(adminLogin) == null)
            {
                User admin = new User { Email = adminLogin, UserName = adminLogin , Password=password};
                IdentityResult result = await userManager.CreateAsync(admin, password);
                repositoryUser.AddUser(admin);
                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}

