using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Infrastructure.ViewEmployeeData.Persistence
{
    internal class ViewEmployeeDataConfiguration : IEntityTypeConfiguration<Domain.ViewEmployeeData>
    {
        public void Configure(EntityTypeBuilder<Domain.ViewEmployeeData> builder)
        {
            builder.HasNoKey()
                .ToView("ViewEmployeeData");
        }
    }
}
