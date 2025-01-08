using People.Application;
using People.Infrastructure;

namespace People.Api.Extensions
{
    public static class ConfigureService
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddApplicationServices()
                .AddInfrastructureServices(configuration);

            return services;
        }
    }
}
