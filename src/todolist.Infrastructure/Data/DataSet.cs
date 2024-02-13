using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using todolist.Domain;
using static BCrypt.Net.BCrypt;

namespace todolist.Infrastructure.Data;

public class DataSet
{
    public static void IncludeData(IServiceProvider serviceProvider)
    {
        using (var context = serviceProvider.GetRequiredService<DataContext>())
        {
            Console.WriteLine("Applying migrations...");
            context.Database.Migrate();

            if (!context.Usuarios.Any())
            {
                Console.WriteLine("Creating initial data...");
                context.Usuarios.Add(new Usuario() { Nome = "Admin", Email = "admin@admin.com", Senha = HashPassword("@Admin1", 10) });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Database already contains data...");
            }

        }
    }
}