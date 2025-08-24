using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.Interfaces;
using Project.Domain.Common.Interfaces;
using Project.Infrastructure.Authentication.PasswordHasher;
using Project.Infrastructure.Authentication.TokenGenerator;
using Project.Infrastructure.Categories.Persistence;
using Project.Infrastructure.DBContext;
using Project.Infrastructure.Employee.Persistence;
using Project.Infrastructure.EmployeeTerritorie.Persistence;
using Project.Infrastructure.FilePaths.Persistence;
using Project.Infrastructure.Products.Persistence;
using Project.Infrastructure.Supplier.Persistence;
using Project.Infrastructure.Territories.Persistence;
using Project.Infrastructure.UniteOfWork.Persistence;
using Project.Infrastructure.Users.Persistence;

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
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.jwtsettings));
            //services.AddOptions<JwtSettings>()
            //    .Bind(configuration.GetSection("JWT"))
            //    .ValidateOnStart();
            return services;
        }
    }
}
