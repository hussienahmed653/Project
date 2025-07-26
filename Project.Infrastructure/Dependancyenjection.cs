using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.Interfaces;
using Project.Application.Employee.Commands.CreateEmployee;
using Project.Application.Mapping.Employee;
using Project.Infrastructure.DBContext;
using Project.Infrastructure.Employee.Persistence;
using Project.Infrastructure.FilePaths.Persistence;

namespace Project.Infrastructure
{
    public static class Dependancyenjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(option => 
                                                        option.UseSqlServer(connectionstring));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEntityFileRepository ,EntityFileRepository>();
            services.AddMediatR(crf =>
                {
                    crf.RegisterServicesFromAssemblies(typeof(CreateEmployeeCommand).Assembly);
                });
            services.AddAutoMapper(typeof(EmployeeProfile).Assembly);
            return services;
        }
    }
}
