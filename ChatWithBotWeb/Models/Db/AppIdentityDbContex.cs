using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Db
{
    public class AppIdentityDbContex:IdentityDbContext<User>
    {
        public AppIdentityDbContex (DbContextOptions <AppIdentityDbContex> options):base(options)
        {

        }
    }
}
