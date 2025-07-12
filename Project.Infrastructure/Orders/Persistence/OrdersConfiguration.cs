using Microsoft.EntityFrameworkCore;
using Project.Domain;

namespace Project.Infrastructure.Orders.Persistence
{
    internal class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderID);

            builder.ToTable("Orders");

            builder.Property(o => o.OrderID)
                .ValueGeneratedNever();

            builder.HasOne(o => o.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.CustomerID);

            builder.HasOne(o => o.Employee)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.EmployeeID);

            builder.HasOne(o => o.Shipper)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.ShipVia);
        }
    }
}
