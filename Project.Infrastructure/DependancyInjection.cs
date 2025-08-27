using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
using Project.Infrastructure.UserPasswordHistory;
using Project.Infrastructure.Users.Persistence;
using System.Text;
using System.Text.Json.Serialization;

namespace Project.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddPersistence(configuration)
                .AddAuthentication(configuration);
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(option =>
                                                        option.UseSqlServer(connectionstring));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEntityFileRepository, EntityFileRepository>();
            services.AddScoped<IUnitOfWork, UniteOfWorkRepository>();
            services.AddScoped<IEmployeeTerritorieRepository, EmployeeTerritorieRepository>();
            services.AddScoped<ITerritorieRepository, TerritorieRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICatecoryRepository, CatecoryRepository>();
            services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserPasswordHistorieRepository, UserPasswordHistorieRepository>();
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtsetting = new JwtSettings();
            configuration.Bind(JwtSettings.jwtsettings, jwtsetting);
            //services.AddSingleton(Options.Create(jwtsetting));
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.jwtsettings));

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();


            //services.AddOptions<JwtSettings>()
            //    .Bind(configuration.GetSection("JWT"))
            //    .ValidateOnStart();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidAudience = jwtsetting.Audience,
                        ValidIssuer = jwtsetting.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting.Key)),
                    };
                });

            services.AddControllers().AddJsonOptions(opiton =>
            {
                opiton.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
                    
            return services;
        }

        public static IServiceCollection AddSwaggerWithAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyProject",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            return services;
        }
    }
}
