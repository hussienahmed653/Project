using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Employee.Commands.CreateEmployee;
using System.Reflection;

namespace Project.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(crf =>
            {
                crf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
