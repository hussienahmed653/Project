using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;

namespace Project.Application.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ErrorOr<bool>>
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

        public async Task<ErrorOr<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if (!await _employeeRepository.ExistAsync(request.Guid))
                    return Error.NotFound(description: "There is no Employee with this guid");
                var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.Guid);

                await _employeeRepository.DeleteEmployeeAsync(employee.EmployeeID);
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
