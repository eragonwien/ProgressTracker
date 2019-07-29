using SNGCommon.Authentication;
using System.Linq;

namespace ProgressTracker.MVC.Models
{
   public class DbInitializer
   {
      public static void Initialize(PROGRESSTRACKERContext context)
      {
         if (context.Ptproject.Any())
         {
            return;
         }
         CreateTestUser(context);
         CreateTestProject(context);
      }

      private static void CreateTestUser(PROGRESSTRACKERContext context)
      {
         Ptuser user = new Ptuser
         {
            Email = "test@test.com",
            Name = "Tester",
            Password = Authentication.GetEncodedPassword("test"),
            Description = "This is a test user",
            Active = true
         };
         context.Ptuser.Add(user);
         context.SaveChanges();
      }

      private static void CreateTestProject(PROGRESSTRACKERContext context)
      {
         Ptproject project = new Ptproject
         {
            Name = "Test Project",
            Description = "This is a test project",
            Ptuser = context.Ptuser.First(),
            Status = ProjectStatus.None.ToString(),
            Active = true
         };
         context.Ptproject.Add(project);
         context.SaveChanges();

         Pttask task = new Pttask
         {
            Description = "First Task",
            PtprojectId = project.Id,
            Completed = false,
            Active = true
         };
         context.Pttask.Add(task);
         context.SaveChanges();
      }
   }
}
