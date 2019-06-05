using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ProgressTracker.Middlewares
{
   // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
   public class LogException
   {
      private readonly RequestDelegate _next;
      private readonly ILogger<LogException> log;

      public LogException(RequestDelegate next, ILogger<LogException> log)
      {
         _next = next;
         this.log = log;
      }

      public Task Invoke(HttpContext httpContext)
      {
         try
         {
            return _next(httpContext);
         }
         catch (Exception ex)
         {
            log.LogError("{0}: {1}", ex.Message, ex.StackTrace);
            throw;
         }
      }
   }

   // Extension method used to add the middleware to the HTTP request pipeline.
   public static class LogExceptionExtensions
   {
      public static IApplicationBuilder UseLogException(this IApplicationBuilder builder)
      {
         return builder.UseMiddleware<LogException>();
      }
   }
}
