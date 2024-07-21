using TaskManagementApp.Data;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Commands.DeleteTask;

public class DeleteTaskCommand
{
    public int TaskId { get; set; }
    private readonly ITaskService _taskService;

    public DeleteTaskCommand(ITaskService taskService)
    {
        _taskService = taskService;
    }
    public async Task Handle()
    {

        await _taskService.DeleteTaskAsync(TaskId);
    }
}