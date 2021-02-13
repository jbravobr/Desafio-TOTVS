using Desafio.Core.Domain.Models;
using Desafio.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
{
    public static class WebHostExtensions
    {
        public static IHost MigrationAndSeed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<DatabaseContext>();
                context.Database.Migrate();
                Seed(context);
            }

            return host;
        }

        public static void Seed(DatabaseContext context)
        {
            context.Users.Add(new User() { Email = "jbravo.br@gmail.com", Password = "P@ssw0rd!", Enabled = true });
            context.SaveChanges();
        }
    }
}