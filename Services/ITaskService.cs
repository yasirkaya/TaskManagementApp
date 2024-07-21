using TaskManagementApp.Models;
using static TaskManagementApp.Aplication.Queries.GetFilteredTasks.GetFilteredTasksQuery;

public interface ITaskService
{
    Task<TaskItem> GetTaskByIdAsync(int id);
    Task<IEnumerable<TaskItem>> GetAllTasksAsync();
    Task AddTaskAsync(TaskItem task);
    Task UpdateTaskAsync(TaskItem task);
    Task DeleteTaskAsync(int id);
    Task<IEnumerable<TaskItem>> GetFilteredTaskAsync(GetTasksFilterModel filter);
}