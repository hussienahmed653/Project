using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;

namespace Project.Application.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ErrorOr<bool>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityFileRepository _entityFileRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEntityFileRepository entityFileRepository)
        {
            _employeeRepository = employeeRepository;
            _entityFileRepository = entityFileRepository;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.Guid);
                if (employee is null)
                    throw new KeyNotFoundException("There is no Employee With This Guid");
                await _employeeRepository.DeleteEmployeeAsync(employee.EmployeeID);
                await _entityFileRepository.DeleteFileAsync(employee.EmployeeGuid);

                return true;
            }
            catch(Exception ex)
            {
                return Error.Failure("DeleteEmployee", ex.Message);
            }

        }
    }
}
