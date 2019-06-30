using System;
using System.Collections.Generic;
using System.Globalization;
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
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgressTracker.Models;
using ProgressTracker.Services;
using SNGCommon.Middlewares;
using SNGCommon;

namespace ProgressTracker
{
   public class Startup
   {
      public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
      {
         Configuration = configuration;
         HostingEnvironment = hostingEnvironment;
      }

      public IConfiguration Configuration { get; private set; }
      public IHostingEnvironment HostingEnvironment { get; private set; }

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
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
               options.SlidingExpiration = true;
               options.Cookie.HttpOnly = false;
               options.LoginPath = "/Login";
               options.LogoutPath = "/Logout";
            });

         List<CultureInfo> cultureInfos = new List<CultureInfo> { new CultureInfo(Settings.Culture_EN), new CultureInfo(Settings.Culture_DE) };
         // Language
         services.AddLocalization();
         services.Configure<RequestLocalizationOptions>(options =>
         {
            options.DefaultRequestCulture = new RequestCulture(Settings.Culture_EN);
            options.SupportedCultures = cultureInfos;
            options.SupportedUICultures = cultureInfos;
         });

         services.AddAntiforgery(s => s.HeaderName = "XSRF-TOKEN");
         services
            .AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddViewLocalization()
            .AddDataAnnotationsLocalization()
            .AddRazorPagesOptions(options =>
            {
               options.Conventions.AddPageRoute("/Projects", "");
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
            app.UseLogException();
            app.UseExceptionHandler("/Error");
            app.UseHsts();
         }

         app.UseRequestLocalization();
         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseAuthentication();
         app.UseMvc();
      }
   }
}
