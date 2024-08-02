using System.Text.Json;
using TaskManagementApp.Models;
using static TaskManagementApp.Aplication.Queries.GetFilteredTasks.GetFilteredTasksQuery;

namespace TaskManagementApp.Services.TaskService;

public class CacheTaskServiceDecorator : ITaskService
{
    private readonly ITaskService _taskService;
    private readonly ICacheService _cacheService;

    public CacheTaskServiceDecorator(ITaskService taskService, ICacheService cacheService)
    {
        _taskService = taskService;
        _cacheService = cacheService;
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        await _taskService.AddTaskAsync(task);
        await _cacheService.RemoveAsync("tasks");
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskService.DeleteTaskAsync(id);
        await _cacheService.RemoveAsync("tasks");
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        string cacheKey = "tasks";
        var cachedTask = await _cacheService.GetAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedTask))
        {
            return JsonSerializer.Deserialize<List<TaskItem>>(cachedTask);
        }

        var tasks = await _taskService.GetAllTasksAsync();

        if (tasks is not null)
        {
            await _cacheService.SetAsync(cacheKey, JsonSerializer.Serialize(tasks));
        }

        return tasks;
    }

    public async Task<IEnumerable<TaskItem>> GetFilteredTaskAsync(GetTasksFilterModel filter)
    {
        string cacheKey = $"tasks_filter_{filter.Name}_{filter.IsCompleted}_{filter.DueDateStart}_{filter.DueDateEnd}";
        var cachedTasks = await _cacheService.GetAsync(cacheKey);

        if (cachedTasks != null)
        {
            return JsonSerializer.Deserialize<List<TaskItem>>(cachedTasks);
        }

        var tasks = await _taskService.GetFilteredTaskAsync(filter);
        if (tasks != null)
        {
            await _cacheService.SetAsync(cacheKey, JsonSerializer.Serialize(tasks));
        }

        return tasks;
    }

    public async Task<TaskItem> GetTaskByIdAsync(int id)
    {
        var cacheKey = $"task_{id}";
        var cachedTask = await _cacheService.GetAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedTask))
        {
            return JsonSerializer.Deserialize<TaskItem>(cachedTask);
        }

        var task = await _taskService.GetTaskByIdAsync(id);
        if (task != null)
        {
            await _cacheService.SetAsync(cacheKey, JsonSerializer.Serialize(task), TimeSpan.FromMinutes(5));
        }

        return task;
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        await _taskService.UpdateTaskAsync(task);
        await _cacheService.RemoveAsync("tasks");
        await _cacheService.RemoveAsync($"task_{task.Id}");
    }


}
