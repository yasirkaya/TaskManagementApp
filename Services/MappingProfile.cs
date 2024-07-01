using AutoMapper;
using TaskManagementApp.Models;
using static TaskManagementApp.Aplication.Commands.AddTask.AddTaskCommand;
using static TaskManagementApp.Aplication.Commands.UpdateTask.UpdateTaskCommand;

namespace TaskManagementApp.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddTaskModel, TaskItem>();
        CreateMap<UpdateTaskModel, TaskItem>();
    }
}