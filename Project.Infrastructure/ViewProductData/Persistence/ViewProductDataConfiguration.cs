using Microsoft.EntityFrameworkCore;
using ViewProduct =  Project.Domain.ViewClasses.ViewProductData;

namespace Project.Infrastructure.ViewProductData.Persistence
{
    internal class ViewProductDataConfiguration : IEntityTypeConfiguration<ViewProduct>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ViewProduct> builder)
        {
            builder.HasNoKey()
                .ToView("ViewProductData");
        }
    }
}
