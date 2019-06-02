using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgressTracker.Models;
using ProgressTracker.Services;

namespace ProgressTracker
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
         // DB Context
         services.AddDbContext<PROGRESSTRACKERContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PROGRESSTRACKER")));
         services.AddScoped<IUserService, UserService>();
         services.AddScoped<IProjectService, ProjectService>();
         services.AddScoped<ITaskService, TaskService>();

         // Authentication Default
         services
            .AddAuthentication(options =>
            {
               options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
               options.SlidingExpiration = true;
               options.Cookie.HttpOnly = false;
               options.LoginPath = "/Login";
               options.LogoutPath = "/Logout";
            })
            .AddGoogle(options =>
            {
               options.ClientId = Configuration["Authentication:Google:ClientId"];
               options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });


         services.AddAntiforgery(s => s.HeaderName = "XSRF-TOKEN");
         services
            .AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddRazorPagesOptions(options =>
            {
               options.Conventions.AuthorizePage("/Index");
               options.Conventions.AllowAnonymousToPage("/Login");
               options.Conventions.AuthorizePage("/Login/LoginRedirect");
               options.Conventions.AuthorizePage("/Project");
               options.Conventions.AuthorizePage("/Task");
            });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
         }

         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseAuthentication();
         app.UseMvc();
      }
   }
}
