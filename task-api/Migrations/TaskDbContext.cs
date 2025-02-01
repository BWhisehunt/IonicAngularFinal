using Microsoft.EntityFrameworkCore;

namespace TaskApi.Migrations;

public class TaskDbContext : DbContext
{
    public DbSet<Models.Task> Tasks { get; set; }
    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Models.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Completed).IsRequired();
        });
                modelBuilder.Entity<Models.Task>().HasData(
            new Models.Task { 
                TaskId = 1,
                Title = "Create Task Dialog",
                Completed = false
            },
            new Models.Task { 
                TaskId = 2,
                Title = "Delete Tasks",
                Completed = false
            },
            new Models.Task { 
                TaskId = 3,
                Title = "Styling & Theme",
                Completed = false
            },
            new Models.Task { 
                TaskId = 4,
                Title = "Create REST API Endpoint",
                Completed = true
            },
            new Models.Task { 
                TaskId = 5,
                Title = "Display Task List",
                Completed = true
            },
            new Models.Task { 
                TaskId = 6,
                Title = "Toggle Tasks",
                Completed = true
            }
        );
    }
}