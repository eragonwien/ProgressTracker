using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using ProgressTracker.Models;
using ProgressTracker.Services;

namespace ProgressTracker
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var logger = NLogBuilder.ConfigureNLog(Settings.NlogConfigFileName).GetCurrentClassLogger();
         try
         {
            logger.Info("Program's Initialization starts");
            var host = CreateWebHostBuilder(args).Build();
            InitializeDb(host);
            host.Run();
            logger.Info("Program's Initialization completed without error");
         }
         catch (Exception ex)
         {
            logger.Error(ex, "Program stopped due to exception");
            throw;
         }
         finally
         {
            NLog.LogManager.Shutdown();
         }

      }

      private static void InitializeDb(IWebHost host)
      {
         using (var scope = host.Services.CreateScope())
         {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<PROGRESSTRACKERContext>();
            DbInitializer.Initialize(context);
         }
      }

      public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>()
              .ConfigureLogging(logging =>
              {
                 logging.ClearProviders();
                 logging.SetMinimumLevel(LogLevel.Debug);
              })
              .UseNLog();
   }
}
