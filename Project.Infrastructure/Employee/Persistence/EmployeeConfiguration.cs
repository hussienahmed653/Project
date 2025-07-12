using Microsoft.EntityFrameworkCore;
using Project.Domain;

namespace Project.Infrastructure.Employee.Persistence
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Employee> builder)
        {
            builder.HasKey(e => e.EmployeeID);

            builder.Property(e => e.EmployeeID)
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasOne(e => e.Manager)
                .WithMany(e => e.employee)
                .HasForeignKey(e => e.ReportsTo);
        }
    }
}
