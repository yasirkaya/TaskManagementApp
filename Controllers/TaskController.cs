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
using TaskManagementApp.Models;
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
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredTasks([FromQuery] GetTasksFilterModel filter)
    {
        var tasks = await _taskService.GetFilteredTaskAsync(filter);
        return Ok(tasks);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] AddTaskModel newTask)
    {
        TaskItem task = _mapper.Map<TaskItem>(newTask);
        await _taskService.AddTaskAsync(task);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskModel updateTask)
    {
        var task = _mapper.Map<TaskItem>(updateTask);
        task.Id = id;
        await _taskService.UpdateTaskAsync(task);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await _taskService.DeleteTaskAsync(id);
        return Ok();
    }
}