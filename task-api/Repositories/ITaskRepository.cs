using Task = TaskApi.Models.Task;

namespace TaskApi.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<Task> GetAllTask();
        Task? GetTaskById(int taskId);
        Task CreateTask(Task newTask);
        Task? UpdateTask(Task newTask);
        void DeleteTaskById(int taskId);

        // New methods for filtering tasks
        IEnumerable<Task> GetCompletedTasks();
        IEnumerable<Task> GetIncompleteTasks();
        object CreateTask(System.Threading.Tasks.Task task);
    }
}