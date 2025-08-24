using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs;
using Project.Domain;
using Project.Domain.Authentication;
using System.Reflection;

namespace Project.Infrastructure.DBContext
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Domain.Employee> Employees { get; set; }
        public DbSet<Domain.EmployeeTerritorie> EmployeeTerritories { get; set; }
        public DbSet<Territorie> Territorie { get; set; }
        public DbSet<Domain.Region> Regions { get; set; }
        public DbSet<Domain.Product> Product { get; set; }
        public DbSet<Domain.Categories> Categories { get; set; }
        public DbSet<Domain.Supplier> Supplier { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.CustomerDemographics> CustomerDemographics { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<Domain.ViewClasses.ViewEmployeeData> viewEmployeeDatas { get; set; }
        public DbSet<Domain.ViewClasses.ViewProductData> viewProductDatas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
