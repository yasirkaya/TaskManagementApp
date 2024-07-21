using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Queries.GetTasks;

public class GetTasksQuery
{
    private readonly TaskService _taskService;

    public GetTasksQuery(TaskService taskService)
    {
        _taskService = taskService;
    }
    public async Task<IEnumerable<TaskItem>> Handle()
    {
        return await _taskService.GetAllTasksAsync();
    }
}