using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace RealTimeTasksReact.Data
{
    public class TaskDataContext : DbContext
    {
        private readonly string _connectionString;

        public TaskDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.tasks)
                .HasForeignKey(t => t.UserId);
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
