using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Infrastructure.ViewEmployeeData.Persistence
{
    internal class ViewEmployeeDataConfiguration : IEntityTypeConfiguration<Domain.ViewEmployeeData>
    {
        public void Configure(EntityTypeBuilder<Domain.ViewEmployeeData> builder)
        {
            builder.HasNoKey()
                .ToView("ViewEmployeeData");

            /*
             
            create view ViewEmployeeData as
            select e.* , f.Path, T.*,R.RegionDescription
            from Employees e
            left join FilePaths f
            on e.EmployeeGuid = f.EntityGuid
            left join EmployeeTerritories ET
            on e.EmployeeID = ET.EmployeeID
            left join Territorie T
            on ET.TerritoryID = T.TerritoryID
            left join Regions R
            on R.RegionID = T.RegionID
             
         */
        }
    }
}
