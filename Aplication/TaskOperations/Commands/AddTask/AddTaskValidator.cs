using FluentValidation;
using TaskManagementApp.Models;

namespace TaskManagementApp.Aplication.Commands.AddTask;

public class AddTaskValidator : AbstractValidator<TaskItem>
{
    public AddTaskValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(200);
        RuleFor(x => x.DueDate).GreaterThan(DateTime.Now);
    }
}