using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Queries.GetFilteredTasks;

public class GetFilteredTasksQuery
{
    private readonly AppDbContext _context;

    public GetFilteredTasksQuery(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> Handle(GetTasksFilterModel filter)
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

    public class GetTasksFilterModel
    {
        public string? Name { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? DueDateStart { get; set; }
        public DateTime? DueDateEnd { get; set; }
    }
}