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
using TaskManagementApp.Services.TaskService;
using static TaskManagementApp.Aplication.Commands.AddTask.AddTaskCommand;
using static TaskManagementApp.Aplication.Commands.UpdateTask.UpdateTaskCommand;
using static TaskManagementApp.Aplication.Queries.GetFilteredTasks.GetFilteredTasksQuery;

namespace TaskManagementApp.Controllers;

[ApiController]
[Route("api/[controller]s")]

public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;

    public TaskController(IMapper mapper, ITaskService taskService)
    {
        _mapper = mapper;
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] GetTasksFilterModel filter)
    {
        GetFilteredTasksQuery query = new GetFilteredTasksQuery(_taskService);
        var result = await query.Handle(filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        GetTaskDetailQuery query = new GetTaskDetailQuery(_taskService);
        query.TaskId = id;
        var result = await query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] AddTaskModel newTask)
    {
        AddTaskCommand command = new AddTaskCommand(_mapper, _taskService);
        command.Model = newTask;
        await command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskModel updateTask)
    {
        UpdateTaskCommand command = new UpdateTaskCommand(_taskService, _mapper);
        command.TaskId = id;
        command.Model = updateTask;
        await command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        DeleteTaskCommand command = new DeleteTaskCommand(_taskService);
        command.TaskId = id;
        await command.Handle();
        return Ok();
    }
}