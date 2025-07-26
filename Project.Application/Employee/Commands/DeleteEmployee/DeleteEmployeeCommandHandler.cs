using MediatR;
using Project.Application.Common.Interfaces;

namespace Project.Application.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityFileRepository _entityFileRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEntityFileRepository entityFileRepository)
        {
            _employeeRepository = employeeRepository;
            _entityFileRepository = entityFileRepository;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
                var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.Guid);
                if (employee is null)
                    throw new FileNotFoundException("There is no Employee With This Guid");
                await _employeeRepository.DeleteEmployeeAsync(employee.Employee.EmployeeID);
                await _entityFileRepository.DeleteFileAsync(employee.Employee.EmployeeGuid);

                return true;

        }
    }
}
