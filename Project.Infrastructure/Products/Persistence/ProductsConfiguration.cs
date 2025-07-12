using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain;
using System.Text;

namespace Project.Infrastructure.Products.Persistence
{
    internal class ProductsConfiguration : IEntityTypeConfiguration<Domain.Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductID);

            builder.Property(p => p.ProductID)
                .ValueGeneratedNever();

            //builder.Property(p => new {p.UnitPrice, p.UnitsInStock, p.UnitsOnOrder, p.ReorderLevel })
            //    .HasDefaultValue(0);

            builder.Property(p => p.UnitPrice)
                .HasDefaultValue(0m);

            builder.Property(p => p.UnitsInStock)
                .HasDefaultValue((byte)0);

            builder.Property(p => p.UnitsOnOrder)
                .HasDefaultValue((byte)0);

            builder.Property(p => p.ReorderLevel)
                .HasDefaultValue((byte)0);

            builder.Property(p => p.Discontinued)
                .HasDefaultValue(0);

            builder.HasOne(p => p.supplier)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.SupplierID);

            builder.HasOne(p => p.category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryID);
        }
    }
}
