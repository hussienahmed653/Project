using Microsoft.EntityFrameworkCore;
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
    }
}
