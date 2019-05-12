using System;
using System.Linq;
using System.Threading.Tasks;
using SNGCommon.Authentication;

namespace ProgressTracker.Models
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
                Name = "Tester",
                Description = "This is a test user",
                Email = "test@test.com",
                Password = Authentication.GetEncodedPassword("test"),
                IsActive = true
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
                Status = Status.None.ToString(),
                IsActive = true
            };
            context.Ptproject.Add(project);
            context.SaveChanges();

            Ptobjective objective = new Ptobjective
            {
                Description = "First objective",
                PtprojectId = project.Id,
                IsCompleted = false,
                IsActive = true
            };
            context.Ptobjective.Add(objective);
            context.SaveChanges();
        }
    }
}
