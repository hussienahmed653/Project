using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using userpasswordhistory = Project.Domain.TrigerUpdateUserTable.UserPasswordHistory;
namespace Project.Infrastructure.UserPasswordHistory
{
    internal class UserPasswordHistoryConfiguration : IEntityTypeConfiguration<userpasswordhistory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<userpasswordhistory> builder)
        {
            builder.HasKey(u => new { u.UserGuid, u.OldPassword });

            builder.Property(u => u.UserGuid)
                .ValueGeneratedOnAdd();
                //.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
            builder.Property(u => u.OldPassword)
                .ValueGeneratedOnAdd();
                //.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            
            //builder.ToTable(u => u.UseSqlOutputClause(false));
        }
    }
}
