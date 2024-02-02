using Microsoft.EntityFrameworkCore;
using todolist.Data;

namespace todolist.DependencyInjection;

internal static class Initialization
{
    public static IApplicationBuilder InitializeData(this IApplicationBuilder applicationBuilder)
    {
        using (var scope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                context.Database.Migrate();
                DataSet.IncludeData(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error ocurred while trying to initialize the database...");
            }
        }
        return applicationBuilder;
    }
}