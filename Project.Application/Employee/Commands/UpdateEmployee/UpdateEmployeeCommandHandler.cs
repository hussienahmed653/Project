using ErrorOr;
using Mapster;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;

namespace Project.Application.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ErrorOr<EmployeeResponseDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityFileRepository _genericUploadeEntityFile;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
            IEntityFileRepository genericUploadeEntityFile,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _genericUploadeEntityFile = genericUploadeEntityFile;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<EmployeeResponseDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.UpdateEmployeeDTO.EmployeeGuid);
                if (employee is null)
                    return Error.NotFound(code: "Not Found", description: "There is no Employee With this Guid");
                request.UpdateEmployeeDTO.Adapt(employee);

                //await _employeeRepository.UpdateEmployeeAsync(employee);
                var employeemapper = employee.Adapt<EmployeeResponseDto>();
                await _unitOfWork.CommitAsync();
                return employeemapper;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("Something went wrong while updateing Employee");
            }
        }
    }
}
