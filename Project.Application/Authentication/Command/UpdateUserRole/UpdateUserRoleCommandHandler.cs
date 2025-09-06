using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Authentication.Command.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandlerRepository<UpdateUserRoleCommand, ErrorOr<Updated>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UpdateUserRoleCommandHandler(IUnitOfWork unitOfWork,
                                            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateUserRoleCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                //if (!await _userRepository.ExistByEmail(request.email))
                //    return Error.Conflict(description: ".بيانات تسجيل دخولك لا تتناسب مع اي حساب في سجلاتنا");
                //var user = await _userRepository.GetUserByEmail(request.email);
                //user.Role = request.Roles;
                var updated = await _userRepository.Update(request.email, (int)request.Roles);
                if(!updated)
                    return Error.Conflict(description: ".بيانات تسجيل دخولك لا تتناسب مع اي حساب في سجلاتنا");
                await _unitOfWork.CommitAsync();
                return Result.Updated;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("");
            }
        }
    }
}
