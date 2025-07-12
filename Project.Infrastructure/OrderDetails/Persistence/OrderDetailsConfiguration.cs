using Microsoft.EntityFrameworkCore;

namespace Project.Infrastructure.OrderDetails.Persistence
{
    internal class OrderDetailsConfiguration : IEntityTypeConfiguration<Domain.OrderDetails>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.OrderDetails> builder)
        {
            builder.HasKey(o => new {o.OrderID, o.ProductID });

            builder.HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderID);

            builder.HasOne(o => o.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(o => o.ProductID);
        }
    }
}
