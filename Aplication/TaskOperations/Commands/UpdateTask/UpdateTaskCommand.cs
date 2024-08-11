using System.Diagnostics.Eventing.Reader;
using AutoMapper;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Commands.UpdateTask;

public class UpdateTaskCommand
{
    public UpdateTaskModel Model { get; set; }
    public int TaskId { get; set; }
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public UpdateTaskCommand(AppDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task Handle()
    {
        var task = _mapper.Map<TaskItem>(Model);
        task.Id = TaskId;

        var existingTask = await _context.Task.FindAsync(task.Id);

        if (existingTask is not null)
        {
            existingTask.Name = (string.IsNullOrEmpty(task.Name.Trim()) || task.Name == "string") ? existingTask.Name : task.Name;
            existingTask.Description = (string.IsNullOrEmpty(task.Description) || task.Description == "string") ? existingTask.Description : task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            existingTask.DueDate = task.DueDate.ToString("g") == DateTime.Now.ToString("g") ? existingTask.DueDate : task.DueDate;

            await _context.SaveChangesAsync();
        }

    }

    public class UpdateTaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}