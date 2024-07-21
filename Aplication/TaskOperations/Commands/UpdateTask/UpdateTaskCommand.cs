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
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;
    public UpdateTaskCommand(ITaskService taskService, IMapper mapper)
    {
        _taskService = taskService;
        _mapper = mapper;
    }
    public async Task Handle()
    {
        var task = _mapper.Map<TaskItem>(Model);
        await _taskService.UpdateTaskAsync(task);

    }

    public class UpdateTaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}