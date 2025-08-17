using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.Categories.Persistence;
using Project.Infrastructure.DBContext;
using Project.Infrastructure.Employee.Persistence;
using Project.Infrastructure.EmployeeTerritorie.Persistence;
using Project.Infrastructure.FilePaths.Persistence;
using Project.Infrastructure.Products.Persistence;
using Project.Infrastructure.Supplier.Persistence;
using Project.Infrastructure.Territories.Persistence;
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
            services.AddScoped<IEmployeeTerritorieRepository, EmployeeTerritorieRepository>();
            services.AddScoped<ITerritorieRepository, TerritorieRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICatecoryRepository, CatecoryRepository>();
            services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            
            return services;
        }
    }
}
