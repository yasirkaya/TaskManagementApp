using TaskManagementApp.Models;
using static TaskManagementApp.Aplication.Queries.GetFilteredTasks.GetFilteredTasksQuery;

namespace TaskManagementApp.Services.TaskService;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllTasksAsync();
    }

    public async Task<TaskItem> GetTaskByIdAsync(int id)
    {
        return await _taskRepository.GetTaskByIdAsync(id);
    }

    public async Task<IEnumerable<TaskItem>> GetFilteredTaskAsync(GetTasksFilterModel filter)
    {
        return await _taskRepository.GetFilteredTaskAsync(filter);
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        await _taskRepository.AddTaskAsync(task);
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        await _taskRepository.UpdateTaskAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskRepository.DeleteTaskAsync(id);
    }
}