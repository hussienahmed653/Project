using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Infrastructure.CustomerCustomerDemo.Persistence
{
    internal class CustomerCustomerDemoConfiguration : IEntityTypeConfiguration<Domain.CustomerCustomerDemo>
    {
        public void Configure(EntityTypeBuilder<Domain.CustomerCustomerDemo> builder)
        {
            builder.HasKey(c => new { c.CustomerID, c.CustomerTypeID });

            builder.HasOne(c => c.Customer)
                .WithMany(c => c.customerCustomerDemos)
                .HasForeignKey(c => c.CustomerID);

            builder.HasOne(c => c.CustomerDemographics)
                .WithMany(c => c.customerCustomerDemos)
                .HasForeignKey(c => c.CustomerTypeID);
        }
    }
}
