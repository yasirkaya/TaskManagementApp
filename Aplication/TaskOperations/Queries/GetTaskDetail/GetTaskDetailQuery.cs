using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Aplication.Queries.GetTaskDetail;

public class GetTaskDetailQuery
{
    public int TaskId { get; set; }
    private readonly AppDbContext _context;

    public GetTaskDetailQuery(AppDbContext context)
    {
        _context = context;
    }

    public TaskItem Handle()
    {
        var task = _context.Task.SingleOrDefault(x => x.Id == TaskId);

        if (task is null)
            throw new InvalidOperationException("Görev Bulunamadı.");

        return task;
    }
}