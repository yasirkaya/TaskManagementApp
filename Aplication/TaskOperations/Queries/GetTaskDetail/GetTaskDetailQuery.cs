using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Queries.GetTaskDetail;

public class GetTaskDetailQuery
{
    public int TaskId { get; set; }
    private readonly ITaskService _taskService;

    public GetTaskDetailQuery(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskItem> Handle()
    {
        return await _taskService.GetTaskByIdAsync(TaskId);
    }
}