using AutoMapper;
using Project.Application.DTOs;

namespace Project.Application.Mapping.Employee
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeWithFiles, EmployeeResponseDto>()
                .ForMember(dest => dest.Filepath, sor => sor.MapFrom(f => f.EntityFiles));
            CreateMap<AddEmployeeDto, Domain.Employee>();
            CreateMap<UpdateEmployeeDto, EmployeeResponseDto>();

            CreateMap<UpdateEmployeeDto, Domain.Employee>()
                .ForAllMembers(option => option.Condition((src , dest, srcmember) => srcmember != null));
        }
    }
}
