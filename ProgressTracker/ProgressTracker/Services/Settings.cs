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

   public static class TranslationSetting
   {
      public const string Cancel = "Cancel";
      public const string Completed = "Completed";
      public const string CreateNewList = "CreateNewList";
      public const string CreateNewTask = "CreateNewTask";
      public const string Email = "Email";
      public const string Login = "Login";
      public const string Logout = "Logout";
      public const string New = "New";
      public const string Register = "Register";
      public const string SaveChanges = "SaveChanges";
      public const string ValdationUserNotFound = "ValdationUserNotFound";
      public const string ValidationEmptyEmail = "ValidationEmptyEmail";
      public const string ValidationEmptyPassword = "ValidationEmptyPassword";
      public const string ValidationInvalidEmail = "ValidationInvalidEmail";
      public const string ValidationPasswordMismatch = "ValidationPasswordMismatch";
      public const string Working = "Working";

   }
}
