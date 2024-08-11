using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Queries.GetTasks;

public class GetTasksQuery
{
    private readonly AppDbContext _context;

    public GetTasksQuery(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TaskItem>> Handle()
    {
        return await _context.Task.OrderBy(x => x.Id).ToListAsync();
    }
}