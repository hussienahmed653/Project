using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;
using Project.Infrastructure.Employee.Persistence;
using Project.Infrastructure.FilePaths.Persistence;
using Project.Infrastructure.UniteOfWork.Persistence;

namespace Project.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(option => 
                                                        option.UseSqlServer(connectionstring));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEntityFileRepository ,EntityFileRepository>();
            services.AddScoped<IUnitOfWork, UniteOfWorkRepository>();
            
            return services;
        }
    }
}
