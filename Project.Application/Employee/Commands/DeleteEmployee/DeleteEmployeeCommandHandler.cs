using MediatR;
using Project.Application.Common.Interfaces;

namespace Project.Application.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityFileRepository _entityFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository
            , IEntityFileRepository entityFileRepository
            , IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _entityFileRepository = entityFileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.Guid);
                if (employee is null)
                    throw new FileNotFoundException("There is no Employee With This Guid");
                await _employeeRepository.DeleteEmployeeAsync(employee.EmployeeID);
                await _entityFileRepository.DeleteFileAsync(employee.EmployeeGuid);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("An error occurred while deleting the employee.");
            }

        }
    }
}
