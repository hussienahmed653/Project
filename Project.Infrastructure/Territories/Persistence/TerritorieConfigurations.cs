using Microsoft.EntityFrameworkCore;

namespace Project.Infrastructure.Territories.Persistence
{
    internal class TerritorieConfigurations : IEntityTypeConfiguration<Domain.Territorie>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Territorie> builder)
        {
            builder.HasKey(t => t.TerritoryID);

            builder.Property(t => t.TerritoryID)
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasOne(t => t.Region)
                .WithMany(r => r.Territories)
                .HasForeignKey(r => r.RegionID);
        }
    }
}
