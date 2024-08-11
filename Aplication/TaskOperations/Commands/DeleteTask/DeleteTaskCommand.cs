using TaskManagementApp.Data;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Commands.DeleteTask;

public class DeleteTaskCommand
{
    public int TaskId { get; set; }
    private readonly AppDbContext _context;

    public DeleteTaskCommand(AppDbContext context)
    {
        _context = context;
    }
    public async Task Handle()
    {

        var task = await _context.Task.FindAsync(TaskId);
        if (task is not null)
        {
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}