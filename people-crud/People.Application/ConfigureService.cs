using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using People.Application.Services;
using People.Application.Services.Interfaces;
using People.Application.Validators;

namespace People.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddServices()
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddValidatorsFromAssemblyContaining<PersonValidator>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}
