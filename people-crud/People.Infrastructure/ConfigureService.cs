using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using People.Domain.Repositories;
using People.Infrastructure.Data;
using People.Infrastructure.Repositories;

namespace People.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PeopleDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("No connection string named [DefaultConnection] found"));
            });

            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonEFRepository>();

            return services;
        }
    }
}
