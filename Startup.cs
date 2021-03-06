using Filmix.Managers.Account;
using Filmix.Managers.Actors;
using Filmix.Managers.Films;
using Filmix.Managers.Genres;
using Filmix.Managers.Roles;
using Filmix.Models.AccountModels;
using Filmix.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmix
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(connection);
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();




            services.AddControllersWithViews();

            //Managers
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IFilmManager, FilmManager>();
            services.AddScoped<IActorManager, ActorManager>();
            services.AddScoped<IGenreManager, GenreManager>();
            services.AddScoped<IRoleManager, RoleManager>();


            //Services
            services.AddScoped<IEmailService,EmailService>();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern:"{controller}/{action}/{id?}",
                    defaults: new { controller="Filmix",action="Index"}
                    );


            });
        }
    }
}
