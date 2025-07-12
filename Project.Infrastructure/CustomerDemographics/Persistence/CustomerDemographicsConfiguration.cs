using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.CustomerDemographics.Persistence
{
    internal class CustomerDemographicsConfiguration : IEntityTypeConfiguration<Domain.CustomerDemographics>
    {
        public void Configure(EntityTypeBuilder<Domain.CustomerDemographics> builder)
        {
            builder.HasKey(c => c.CustomerTypeID);
        }
    }
}
