using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs;
using Project.Domain;
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
        public DbSet<Domain.Region> Regions { get; set; }
        public DbSet<Domain.Categories> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.CustomerDemographics> CustomerDemographics { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
    }
}
