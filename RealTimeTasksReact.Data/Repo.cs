using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RealTimeTasksReact.Data
{
    public class Repo
    {

        private readonly string _connectionString;

        public Repo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            using var context = new TaskDataContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            using var context = new TaskDataContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isCorrectPassword ? user : null;
        }

        public List<TaskItem> GetActiveTasks()
        {
            using var context = new TaskDataContext(_connectionString);
            return context.Tasks.Where(task => task.Status != Status.Done).Include(task => task.User).ToList();

        }


        public TaskItem NewTask(string task)
        {
            TaskItem ti = new() { Task = task, Date = DateTime.Now, Status = 0};
            using var context = new TaskDataContext(_connectionString);
            context.Tasks.Add(ti);
            context.SaveChanges();
            return ti;
        }

        public void TaskTaken(int taskId, User currentUser)
        {
            using var context = new TaskDataContext(_connectionString);
            TaskItem ti = context.Tasks.FirstOrDefault(t => t.Id == taskId);
            ti.Status = Status.Taken;
            ti.UserId = currentUser.Id;
            context.Tasks.Update(ti);
            context.SaveChanges();
        }

        public void TaskDone(int taskId)
        {
            using var context = new TaskDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"UPDATE Tasks SET Status = 2 WHERE Id = { taskId}");

        }


    }
}