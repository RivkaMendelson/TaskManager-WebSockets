using Microsoft.AspNetCore.SignalR;
using RealTimeTasksReact.Data;

namespace RealTimeTasksReact
{

    public class TaskHub : Hub

    {
        public string _connectionString;
        public TaskHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public void LoadTasks()
        {
            var repo = new Repo(_connectionString);
            List<TaskItem> tasks = repo.GetActiveTasks();
            Clients.All.SendAsync("renderTasks", tasks.Select(t => new
            {
                Id = t.Id,
                Task = t.Task,
                UserId = t.UserId,
                User = t.User != null ? $"{t.User.FirstName} {t.User.LastName}" : null,
            }));

        }

        public void NewTask(string task)
        {
            var repo = new Repo(_connectionString);
            TaskItem ti = repo.NewTask(task);
            Clients.All.SendAsync("newTaskReceived", ti);
        }

        public void TakeTask(int taskId)
        {
            var repo = new Repo(_connectionString);

            string user = Context.User.Identity.Name;
            User currentUser = repo.GetByEmail(user);
            repo.TaskTaken(taskId, currentUser);
            LoadTasks();
        }

        public void TaskDone(int taskId)
        {
            var repo = new Repo(_connectionString);
            repo.TaskDone(taskId);
            LoadTasks();
        }
    }
}