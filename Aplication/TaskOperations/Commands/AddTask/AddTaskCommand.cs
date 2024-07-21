using AutoMapper;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp.Aplication.Commands.AddTask;

public class AddTaskCommand
{
    public AddTaskModel Model { get; set; }
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;

    public AddTaskCommand(IMapper mapper, ITaskService taskService)
    {
        _mapper = mapper;
        _taskService = taskService;
    }
    public async Task Handle()
    {
        TaskItem task = _mapper.Map<TaskItem>(Model);
        await _taskService.AddTaskAsync(task);
    }
    public class AddTaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }

}



