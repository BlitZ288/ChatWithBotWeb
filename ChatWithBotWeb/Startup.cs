using BotService;
using ChatWithBotWeb.Models.Db;
using ChatWithBotWeb.Service.ChatService;
using ChatWithBotWeb.Service.ChatService.Interface;
using ChatWithBotWeb.Service.MessageService;
using ChatWithBotWeb.Service.MessageService.Interface;
using ChatWithBotWeb.Service.UserService;
using ChatWithBotWeb.Service.UserService.Interface;
using Domian;
using Domian.Context;
using Domian.Entities;
using Domian.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(
                     Configuration["Data:ChatBotWeb:ConnectionString"]
                ));

            services.AddDbContext<AppIdentityDbContex>(option => option.UseNpgsql(
                    Configuration["Data:ChatBotWebIdentity:ConnectionString"]
               ));
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 4;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<AppIdentityDbContex>()
                .AddDefaultTokenProviders();

            //services.AddTransient<IRepositoryUser, RepositoryUser>();
            ////services.AddTransient<IRepositoryChat, ChatRepository>();
            //services.AddTransient<IRepositoryLogUser, LogUserRepository>();
            //services.AddTransient<IRepositoryMessage, MessageRepository>();
            //services.AddTransient<IRepositoryLogAction, LogActionRepository>();

            services.AddTransient<IChatService, ChatServise>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserSercvice>();
            services.AddTransient<IUnitOfWorck, EFUnitOfWork>();

            services.AddTransient<BotsManager>();
            services.AddMemoryCache();
            services.AddSession();

        }

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
