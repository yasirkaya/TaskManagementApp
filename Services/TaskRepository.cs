using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Aplication.Queries.GetFilteredTasks;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using static TaskManagementApp.Aplication.Queries.GetFilteredTasks.GetFilteredTasksQuery;

namespace TaskManagementApp.Services.TaskRepository;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        await _context.Task.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _context.Task.FindAsync(id);
        if (task is not null)
        {
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await _context.Task.OrderBy(x => x.Id).ToListAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetFilteredTaskAsync(GetTasksFilterModel filter)
    {
        var query = _context.Task.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(t => t.Name.Contains(filter.Name));
        }

        if (filter.IsCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == filter.IsCompleted);
        }

        if (filter.DueDateStart.HasValue)
        {
            query = query.Where(t => t.DueDate >= filter.DueDateStart);
        }

        if (filter.DueDateEnd.HasValue)
        {
            query = query.Where(t => t.DueDate <= filter.DueDateEnd);
        }

        return await query.ToListAsync();
    }

    public async Task<TaskItem> GetTaskByIdAsync(int id)
    {
        var task = await _context.Task.FindAsync(id);
        if (task is null)
            throw new InvalidOperationException("Görev Bulunamadı.");
        return task;
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        var existingTask = await _context.Task.FindAsync(task.Id);

        if (existingTask is not null)
        {
            existingTask.Name = string.IsNullOrEmpty(task.Name.Trim()) ? existingTask.Name : task.Name;
            // if (existingTask.Name == "yasiryasir")
            // {
            //     throw new InvalidOperationException("girdi");
            // }
            existingTask.Description = string.IsNullOrEmpty(task.Description) ? existingTask.Description : task.Description;
            System.Console.WriteLine(existingTask.Description);
            existingTask.IsCompleted = task.IsCompleted;
            existingTask.DueDate = task.DueDate.ToString("g") == DateTime.Now.ToString("g") ? existingTask.DueDate : task.DueDate;
            System.Console.WriteLine(existingTask.DueDate);

            await _context.SaveChangesAsync();
        }

    }
}