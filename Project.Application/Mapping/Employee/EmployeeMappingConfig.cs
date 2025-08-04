using Mapster;
using Project.Application.DTOs;

namespace Project.Application.Mapping.Employee
{
    internal class EmployeeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Employee, EmployeeResponseDto>()
                .Map(dest => dest.Filepath!, sour => sour.EntityFiles!.FirstOrDefault()!.Path)
                .IgnoreNullValues(true);

            config.NewConfig<UpdateEmployeeDto, Domain.Employee>()
                .IgnoreNullValues(true);
        }
    }
}
