using Microsoft.AspNetCore.Mvc;
using TaskApi.Repositories;
using TaskModel = TaskApi.Models.Task;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase 
{
    private readonly ILogger<TaskController> _logger;
    private readonly ITaskRepository _taskRepository;

    public TaskController(ILogger<TaskController> logger, ITaskRepository repository)
    {
        _logger = logger;
        _taskRepository = repository;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<TaskModel>> GetTask() 
    {
        return Ok(_taskRepository.GetAllTask());
    }

    [HttpGet("{taskId:int}")]
    public ActionResult<TaskModel> GetTaskById(int taskId) 
    {
        var task = _taskRepository.GetTaskById(taskId);
        if (task == null) {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public ActionResult<TaskModel> CreateTask([FromBody] TaskModel task) 
    {
        if (!ModelState.IsValid || task == null) {
            return BadRequest(ModelState);
        }
        var newTask = _taskRepository.CreateTask(task);
        return CreatedAtAction(nameof(GetTaskById), new { taskId = newTask.TaskId }, newTask);
    }

    [HttpPut("{taskId:int}")]
    public ActionResult<TaskModel> UpdateTask(int taskId, [FromBody] TaskModel task) 
    {
        if (task == null)
        {
            return BadRequest("Task cannot be null.");
        }

        if (taskId != task.TaskId)
        {
            return BadRequest("Task ID mismatch.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingTask = _taskRepository.GetTaskById(taskId);
        if (existingTask == null)
        {
            return NotFound($"Task with ID {taskId} not found.");
        }

        var updatedTask = _taskRepository.UpdateTask(task);
        if (updatedTask == null)
        {
            return StatusCode(500, "Error updating task. Please try again.");
        }

        return Ok(updatedTask);
    }

    [HttpDelete("{taskId:int}")]
    public ActionResult DeleteTask(int taskId) 
    {
        _taskRepository.DeleteTaskById(taskId); 
        return NoContent();
    }
}