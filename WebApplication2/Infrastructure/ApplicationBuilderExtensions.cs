using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebApplication2.Data;

namespace WebApplication2.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using var services = builder.ApplicationServices.CreateScope();
            var context = services.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
