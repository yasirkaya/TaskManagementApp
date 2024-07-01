using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Aplication.Commands.AddTask;
using TaskManagementApp.Data;
using static TaskManagementApp.Aplication.Commands.AddTask.AddTaskCommand;

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

    [HttpPost]
    public IActionResult AddTask([FromBody] AddTaskModel newTask)
    {
        AddTaskCommand command = new AddTaskCommand(_context, _mapper);
        command.Model = newTask;
        command.Handle();
        return Ok();
    }
}