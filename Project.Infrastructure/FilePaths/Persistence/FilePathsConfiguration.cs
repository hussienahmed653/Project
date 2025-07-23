using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain;

namespace Project.Infrastructure.FilePaths.Persistence
{
    public class FilePathsConfiguration : IEntityTypeConfiguration<FilePath>
    {
        public void Configure(EntityTypeBuilder<FilePath> builder)
        {
            builder.HasKey(fp => new { fp.EntityGuid, fp.Path });
        }
    }
}
