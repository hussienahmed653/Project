using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Authentication;

namespace Project.Infrastructure.Users.Persistence
{
    internal class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Role)
                .HasDefaultValue(Roles.User);

            builder.Property(u => u.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable(u => u.HasTrigger("user_Password_Histories"));
        }
    }
}
