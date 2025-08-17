using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Infrastructure.Categories.Persistence
{
    internal class CategoriesConfiguration : IEntityTypeConfiguration<Domain.Categories>
    {
        public void Configure(EntityTypeBuilder<Domain.Categories> builder)
        {
            builder.HasKey(c => c.CategoryID);
            builder.Property(c => c.CategoryID)
                .ValueGeneratedNever();

        }
    }
}
