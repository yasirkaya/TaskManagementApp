using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Queries.GetTaskDetail;

public class GetTaskDetailQuery
{
    public int TaskId { get; set; }
    private readonly AppDbContext _context;

    public GetTaskDetailQuery(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem> Handle()
    {
        var task = await _context.Task.FindAsync(TaskId);

        if (task is null)
            throw new InvalidOperationException("Görev Bulunamadı.");

        return task;
    }
}