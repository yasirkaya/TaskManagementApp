using System.Net.NetworkInformation;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Aplication.Commands.AddTask;
using TaskManagementApp.Aplication.Commands.DeleteTask;
using TaskManagementApp.Aplication.Commands.UpdateTask;
using TaskManagementApp.Aplication.Queries.GetFilteredTasks;
using TaskManagementApp.Aplication.Queries.GetTaskDetail;
using TaskManagementApp.Aplication.Queries.GetTasks;
using TaskManagementApp.Data;
using TaskManagementApp.Services.TaskService;
using static TaskManagementApp.Aplication.Commands.AddTask.AddTaskCommand;
using static TaskManagementApp.Aplication.Commands.UpdateTask.UpdateTaskCommand;
using static TaskManagementApp.Aplication.Queries.GetFilteredTasks.GetFilteredTasksQuery;

namespace TaskManagementApp.Controllers;

[ApiController]
[Route("api/[controller]s")]

public class TaskController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;

    public TaskController(IMapper mapper, ITaskService taskService, AppDbContext context)
    {
        _mapper = mapper;
        _taskService = taskService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        GetTasksQuery query = new GetTasksQuery(_context);
        var result = await query.Handle();
        return Ok(result);
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredTasks([FromQuery] GetTasksFilterModel filter)
    {
        GetFilteredTasksQuery query = new GetFilteredTasksQuery(_context);
        var result = await query.Handle(filter);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        GetTaskDetailQuery query = new GetTaskDetailQuery(_context) { TaskId = id };
        var result = await query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] AddTaskModel newTask)
    {
        AddTaskCommand command = new AddTaskCommand(_context, _mapper);
        command.Model = newTask;
        await command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskModel updateTask)
    {
        UpdateTaskCommand command = new UpdateTaskCommand(_context, _mapper) { TaskId = id, Model = updateTask };
        await command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        DeleteTaskCommand command = new DeleteTaskCommand(_context) { TaskId = id };
        await command.Handle();
        return Ok();
    }
}