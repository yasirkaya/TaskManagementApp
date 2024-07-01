using System.Diagnostics.Eventing.Reader;
using AutoMapper;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Aplication.Commands.UpdateTask;

public class UpdateTaskCommand
{
    public UpdateTaskModel Model { get; set; }
    public int TaskId { get; set; }
    private readonly AppDbContext _context;
    public UpdateTaskCommand(AppDbContext context)
    {
        _context = context;
    }
    public void Handle()
    {
        var task = _context.Task.SingleOrDefault(x => x.Id == TaskId);
        if (task is null)
            throw new InvalidOperationException("Görev Bulunamadı.");

        task.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? task.Name : Model.Name;
        task.Description = string.IsNullOrEmpty(Model.Description) ? task.Description : Model.Description;
        task.IsCompleted = Model.IsCompleted;
        task.DueDate = Model.DueDate.ToString("g") == DateTime.Now.ToString("g") ? task.DueDate : Model.DueDate;

        _context.SaveChanges();
    }

    public class UpdateTaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}