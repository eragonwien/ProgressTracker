using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgressTracker.MVC.Models;
using ProgressTracker.MVC.Services;
using System.Collections.Generic;
using System.Globalization;

namespace ProgressTracker.MVC
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
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
               options.SlidingExpiration = true;
               options.Cookie.HttpOnly = false;
               options.LoginPath = "/Account/Login";
               options.LogoutPath = "/Account/Logout";
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
            .AddViewLocalization()
            .AddDataAnnotationsLocalization()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
         }

         app.UseRequestLocalization();
         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseAuthentication();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Project}/{action=Index}/{id?}");
         });
      }
   }
}
