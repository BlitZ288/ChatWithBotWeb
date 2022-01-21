using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatWithBotWeb.Models.Db
{
    public class AppIdentityDbContex : IdentityDbContext<User>
    {
        public AppIdentityDbContex(DbContextOptions<AppIdentityDbContex> options) : base(options)
        {

        }
    }
}
