using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandlerRepository<DeleteEmployeeCommand, ErrorOr<Deleted>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteEmployeeCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var deleted = await _employeeRepository.DeleteEmployeeAsync(request.Guid);
                if (deleted is 0)
                    return Error.NotFound("NotFound", "There is no Employee with this guid");

                await _unitOfWork.CommitAsync();
                return Result.Deleted;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("Can't Delete this Employee!");
            }

        }
    }
}
