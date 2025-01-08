using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using People.Infrastructure.Data;

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

            return services;
        }
    }
}
