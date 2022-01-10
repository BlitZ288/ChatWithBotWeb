using ChatWithBotWeb.Models;
using ChatWithBotWeb.Models.Db;
using ChatWithBotWeb.Models.Interface;
using ChatWithBotWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatWithBotWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(
                     Configuration["Data:ChatBotWeb:ConnectionString"]
                ));
            services.AddDbContext<AppIdentityDbContex>(option => option.UseNpgsql(
                    Configuration["Data:ChatBotWebIdentity:ConnectionString"]
               ));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContex>()
                .AddDefaultTokenProviders();
            services.AddTransient<IRepositoryUser, RepositoryUser >();
            services.AddTransient<IRepositoryChat, ChatRepository >();
            services.AddTransient<IRepositoryLogUser, LogUserRepository >();
            services.AddTransient<IRepositoryMessage, MessageRepository>();
            services.AddTransient<IRepositoryBot, BotRepository>();
            services.AddMemoryCache();
            services.AddSession();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {  

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
