using AutoMapper;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Aplication.Commands.AddTask;

public class AddTaskCommand
{
    public AddTaskModel Model { get; set; }
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public AddTaskCommand(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public void Handle()
    {
        //burda mapper kullanılacak.
        TaskItem task = _mapper.Map<TaskItem>(Model);
        _dbContext.Task.Add(task);
        _dbContext.SaveChanges();
    }
    public class AddTaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }

}



