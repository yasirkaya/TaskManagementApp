namespace TaskManagementApp.Data;

public static class DatabaseInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
    }
}