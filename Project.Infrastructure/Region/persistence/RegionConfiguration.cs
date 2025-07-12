using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain;

namespace Project.Infrastructure.Region.persistence
{
    internal class RegionConfiguration : IEntityTypeConfiguration<Domain.Region>
    {
        public void Configure(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.HasKey(r => r.RegionID);
            builder.Property(r => r.RegionID)
                .ValueGeneratedNever()
                .IsRequired();
        }
    }
}
