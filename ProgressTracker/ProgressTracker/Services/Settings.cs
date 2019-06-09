using System;
using System.Collections.Generic;
using System.Globalization;
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

      public const string Culture_EN = "en";
      public const string Culture_DE = "de";
      public const string LocalisationResourcePath = "Resources";
   }
}
