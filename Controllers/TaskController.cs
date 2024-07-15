using System.Net.NetworkInformation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Aplication.Commands.AddTask;
using TaskManagementApp.Aplication.Commands.DeleteTask;
using TaskManagementApp.Aplication.Commands.UpdateTask;
using TaskManagementApp.Aplication.Queries.GetFilteredTasks;
using TaskManagementApp.Aplication.Queries.GetTaskDetail;
using TaskManagementApp.Aplication.Queries.GetTasks;
using TaskManagementApp.Data;
using static TaskManagementApp.Aplication.Commands.AddTask.AddTaskCommand;
using static TaskManagementApp.Aplication.Commands.UpdateTask.UpdateTaskCommand;
using static TaskManagementApp.Aplication.Queries.GetFilteredTasks.GetFilteredTasksQuery;

namespace TaskManagementApp.Controllers;

[ApiController]
[Route("api/[controller]s")]

public class TaskController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TaskController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetTasks([FromQuery] GetTasksFilterModel filter)
    {
        GetFilteredTasksQuery query = new GetFilteredTasksQuery(_context);
        var result = query.Handle(filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id)
    {
        GetTaskDetailQuery query = new GetTaskDetailQuery(_context);
        query.TaskId = id;
        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddTask([FromBody] AddTaskModel newTask)
    {
        AddTaskCommand command = new AddTaskCommand(_context, _mapper);
        command.Model = newTask;
        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, [FromBody] UpdateTaskModel updateTask)
    {
        UpdateTaskCommand command = new UpdateTaskCommand(_context);
        command.TaskId = id;
        command.Model = updateTask;
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        DeleteTaskCommand command = new DeleteTaskCommand(_context);
        command.TaskId = id;
        command.Handle();
        return Ok();
    }
}