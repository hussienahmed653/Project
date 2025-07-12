using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Supplier.Persistence
{
    internal class SuppliersConfiguration : IEntityTypeConfiguration<Domain.Supplier>
    {
        public void Configure(EntityTypeBuilder<Domain.Supplier> builder)
        {
            builder.HasKey(s => s.SupplierID);

            builder.Property(s => s.SupplierID)
                .ValueGeneratedNever()
                .IsRequired();

            builder.ToTable("Suppliers");
        }
    }
}
