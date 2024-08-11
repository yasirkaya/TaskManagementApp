using AutoMapper;
using FluentValidation;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Commands.AddTask;

public class AddTaskCommand
{
    public AddTaskModel Model { get; set; }
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AddTaskCommand(AppDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task Handle()
    {
        TaskItem task = _mapper.Map<TaskItem>(Model);

        AddTaskValidator validator = new AddTaskValidator();

        await validator.ValidateAndThrowAsync(task);
        await _context.Task.AddAsync(task);
        await _context.SaveChangesAsync();
    }
    public class AddTaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }

}



