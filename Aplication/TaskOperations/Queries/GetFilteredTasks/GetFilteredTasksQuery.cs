using AutoMapper;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Queries.GetFilteredTasks;

public class GetFilteredTasksQuery
{
    private readonly ITaskService _taskService;

    public GetFilteredTasksQuery(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<IEnumerable<TaskItem>> Handle(GetTasksFilterModel filter)
    {
        return await _taskService.GetFilteredTaskAsync(filter);
    }

    public class GetTasksFilterModel
    {
        public string? Name { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? DueDateStart { get; set; }
        public DateTime? DueDateEnd { get; set; }
    }
}