using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Infrastructure.EmployeeFile.Persistence
{
    internal class EmployeeFileConfiguration : IEntityTypeConfiguration<Domain.EmployeeFile>
    {
        public void Configure(EntityTypeBuilder<Domain.EmployeeFile> builder)
        {
            builder.HasKey(builder => new { builder.EmployeeID, builder.Path});

            builder.HasOne(e => e.employee)
                .WithMany(e => e.EmployeeFiles)
                .HasForeignKey(e => e.EmployeeID);
        }
    }
}
