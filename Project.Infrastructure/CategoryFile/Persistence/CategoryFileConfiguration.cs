using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.CategoryFile.Persistence
{
    internal class CategoryFileConfiguration : IEntityTypeConfiguration<Domain.CategoryFile>
    {
        public void Configure(EntityTypeBuilder<Domain.CategoryFile> builder)
        {
            builder.HasKey(cf => new { cf.CategoryID, cf.Path });
            builder.HasOne(cf => cf.categories)
                .WithMany(c => c.CategoryFiles)
                .HasForeignKey(cf => cf.CategoryID);
        }
    }
}
