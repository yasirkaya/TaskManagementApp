
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Repositories;
using TaskManagementApp.Services.TaskService;

namespace TaskManagementApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Initialize Database
        // using (var scope = app.Services.CreateScope())
        // {
        //     var services = scope.ServiceProvider;
        //     var context = services.GetRequiredService<AppDbContext>();
        //     DatabaseInitializer.Initialize(context);
        // }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
