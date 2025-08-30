using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.Employee.Commands.CreateEmployee;
using System.Reflection;

namespace Project.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(IRequestHandlerRepository<,>))
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandlerRepository<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });
            return services;
        }
    }
}
