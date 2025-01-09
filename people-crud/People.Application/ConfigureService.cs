using Microsoft.Extensions.DependencyInjection;
using People.Application.Services;
using People.Application.Services.Interfaces;

namespace People.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}
