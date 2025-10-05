using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data;

public class TodoDbContext : DbContext
{
    public DbSet<TodoTask> Task { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoTask>().HasData(
            new TodoTask
            {
                Id = 1,
                Title = "Buy books",
                Description = "Buy books for the next school year",
                Priority = Priority.Low,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            },
            new TodoTask
            {
                Id = 2,
                Title = "Clean home",
                Description = "Need to clean the bed room",
                Priority = Priority.Low,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            },
            new TodoTask
            {
                Id = 3,
                Title = "Takehome assignment",
                Description = "Finish the mid-term assignment",
                Priority = Priority.Low,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            },
            new TodoTask
            {
                Id = 4,
                Title = "Play Cricket",
                Description = "Plan the soft ball cricket match on next Sunday",
                Priority = Priority.Low,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            },
            new TodoTask
            {
                Id = 5,
                Title = "Help Saman",
                Description = "Saman need help with his software project",
                Priority = Priority.Low,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            },
            new TodoTask
            {
                Id = 6,
                Title = "Call Dentist",
                Description = "Schedule a check-up appointment for next month",
                Priority = Priority.Medium,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            },
            new TodoTask
            {
                Id = 7,
                Title = "Grocery Shopping",
                Description = "Need to buy milk, bread, and eggs",
                Priority = Priority.High,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            },
            new TodoTask
            {
                Id = 8,
                Title = "Learn Python",
                Description = "Complete the first two chapters of the online course",
                Priority = Priority.Low,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            }
        );
    }
}