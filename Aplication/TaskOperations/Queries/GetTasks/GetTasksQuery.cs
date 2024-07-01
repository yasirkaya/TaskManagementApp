using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Aplication.Queries.GetTasks;

public class GetTasksQuery
{
    private readonly AppDbContext _context;

    public GetTasksQuery(AppDbContext context)
    {
        _context = context;
    }
    public List<TaskItem> Handle()
    {
        var taskList = _context.Task.OrderBy(x => x.Id).ToList<TaskItem>();
        return taskList;

    }
}