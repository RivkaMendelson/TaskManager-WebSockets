using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RealTimeTasksReact.Data
{
    public class TaskDataContextFactory : IDesignTimeDbContextFactory<TaskDataContext>
    {
        public TaskDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}RealTimeTasksReact.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new TaskDataContext(config.GetConnectionString("ConStr"));
        }
    }
}