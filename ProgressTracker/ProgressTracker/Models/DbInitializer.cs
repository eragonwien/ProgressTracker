using System;
using System.Linq;

namespace ProgressTracker.Models
{
    public class DbInitializer
    {
        public static void Initialize(PROGRESSTRACKERContext context)
        {
            if (context.Project.Any())
            {
                return;
            }
            CreateTestUser(context);
            CreateTestProject(context);
        }

        private static void CreateTestUser(PROGRESSTRACKERContext context)
        {
            User user = new User
            {
                Name = "Tester",
                Description = "This is a test user",
                Email = "test@test.com",
                Password = "test",
                IsActive = true
            };
            context.User.Add(user);
            context.SaveChanges();
        }

        private static void CreateTestProject(PROGRESSTRACKERContext context)
        {
            Project project = new Project
            {
                Name = "Test Project",
                Description = "This is a test project",
                User = context.User.First(),
                Status = Status.None.ToString(),
                IsActive = true
            };
            context.Project.Add(project);
            context.SaveChanges();

            Objective objective = new Objective
            {
                Description = "First objective",
                Status = Status.None.ToString(),
                Project = project,
                IsActive = true
            };
            context.Objective.Add(objective);
            context.SaveChanges();

            Task task = new Task
            {
                Description = "First Task",
                Objective = objective,
                IsCompleted = false,
                IsActive = true
            };
            context.Task.Add(task);
            context.SaveChanges();
        }
    }
}
