using Task = TaskApi.Models.Task;
using TaskApi.Migrations;
using Microsoft.EntityFrameworkCore;

namespace TaskApi.Repositories
{
    public class TaskRepository : ITaskRepository 
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> GetAllTask()
        {
            return _context.Tasks.ToList();
        }

        public Task? GetTaskById(int taskId)
        {
            return _context.Tasks.SingleOrDefault(c => c.TaskId == taskId);
        }

        public Task CreateTask(Task newTask)
        {
            _context.Tasks.Add(newTask);
            _context.SaveChanges();
            return newTask;
        }

public Task? UpdateTask(Task newTask)
{
    var originalTask = _context.Tasks.FirstOrDefault(t => t.TaskId == newTask.TaskId);
    
    if (originalTask == null) return null; // Task not found

    // Update fields
    if (originalTask.Title != newTask.Title || originalTask.Completed != newTask.Completed)
    {
        originalTask.Title = newTask.Title;
        originalTask.Completed = newTask.Completed;
        
        _context.Entry(originalTask).State = EntityState.Modified;
        _context.SaveChanges();
    }

    return originalTask;
}

        public void DeleteTaskById(int taskId)
        {
            var task = _context.Tasks.Find(taskId);
            if (task != null) {
                _context.Tasks.Remove(task); 
                _context.SaveChanges(); 
            }
        }

        public IEnumerable<Task> GetCompletedTasks()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetIncompleteTasks()
        {
            throw new NotImplementedException();
        }

        public object CreateTask(System.Threading.Tasks.Task task)
        {
            throw new NotImplementedException();
        }
    }
}