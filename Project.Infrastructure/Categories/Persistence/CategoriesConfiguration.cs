using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
