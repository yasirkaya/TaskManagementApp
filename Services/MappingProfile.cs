using AutoMapper;
using TaskManagementApp.Models;
using static TaskManagementApp.Aplication.Commands.AddTask.AddTaskCommand;

namespace TaskManagementApp.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddTaskModel, TaskItem>();
    }
}