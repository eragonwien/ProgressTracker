using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.Services
{
   public static class Settings
   {
      public const double CookieMaxAgeInDays = 16;
      public const string DefaultReturnUrl = "/";
      public const string MultiSchemesPolicy = "MultiSchemes";
      public const string NlogConfigFileName = "nlog.config";
      public const string AppSettingClientId = "ClientId";
      public const string AppSettingClientSecret = "ClientSecret";
      public const string AppSettingGoogleAuthenticationSettings = "GoogleAuthenticationSettings";
   }
}
