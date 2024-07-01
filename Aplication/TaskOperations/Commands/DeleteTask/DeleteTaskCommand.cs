using TaskManagementApp.Data;

namespace TaskManagementApp.Aplication.Commands.DeleteTask;

public class DeleteTaskCommand
{
    public int TaskId { get; set; }
    public readonly AppDbContext _context;

    public DeleteTaskCommand(AppDbContext context)
    {
        _context = context;
    }
    public void Handle()
    {
        var task = _context.Task.SingleOrDefault(x => x.Id == TaskId);

        if (task is null)
            throw new InvalidOperationException("Görev BUlunamadı.");

        _context.Task.Remove(task);
        _context.SaveChanges();
    }
}