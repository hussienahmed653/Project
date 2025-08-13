using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Infrastructure.EmployeeTerritorie.Persistence
{
    internal class EmployeeTerritorieConfiguration : IEntityTypeConfiguration<Domain.EmployeeTerritorie>
    {
        public void Configure(EntityTypeBuilder<Domain.EmployeeTerritorie> builder)
        {
            builder.HasKey(et => new { et.EmployeeID , et.TerritoryID});

            builder.ToTable("EmployeeTerritories");

            builder.HasOne(et => et.employee)
                .WithMany(e => e.EmployeeTerritories)
                .HasForeignKey(et => et.EmployeeID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(et => et.territorie)
                .WithMany(t => t.EmployeeTerritories)
                .HasForeignKey(et => et.TerritoryID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
