using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;
using Project.Domain.Authentication;
using Project.Domain.Common.Interfaces;

namespace Project.Application.Authentication.Command.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandlerRepository<ChangePasswordCommand, ErrorOr<Updated>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserPasswordHistorieRepository _passwordHistorieRepository;
        ICurrentUserProvider _currentUserProvider;

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork,
                                            IUserRepository userRepository,
                                            IPasswordHasher passwordHasher,
                                            IUserPasswordHistorieRepository passwordHistorieRepository,
                                            ICurrentUserProvider currentUserProvider)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _passwordHistorieRepository = passwordHistorieRepository;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<ErrorOr<Updated>> Handle(ChangePasswordCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var currentuser = _currentUserProvider.GetCurrentUser();

                var user = await _userRepository.GetUserByEmail(currentuser.email);

                //var differentpasswords = User.CurrentPasswordIsEqualsNewPassword(request.ChangePasswordRequest.CurrentPassword, request.ChangePasswordRequest.NewPassword);

                //if(differentpasswords.IsError)
                //    return differentpasswords.Errors;

                if (_passwordHasher.IsCorrectPassword(request.ChangePasswordRequest.NewPassword, user.PasswordHash))
                    return Error.Conflict(description: ".كلمة المرور الجديدة لا يمكن ان تكون نفس كلمة المرور الحالية");

                var passwordmatch = User.PasswordNoMatched(request.ChangePasswordRequest.NewPassword, request.ChangePasswordRequest.ConfirmNewPassword);
                
                if (passwordmatch.IsError)
                    return passwordmatch.Errors;

                var newpasword = _passwordHasher.HashPassword(request.ChangePasswordRequest.NewPassword);
                
                if(newpasword.IsError)
                    return newpasword.Errors;

                var oldhashers = await _passwordHistorieRepository.ExistByPasswordHash(user.Id); //

                if(!User.CanUseThisPassword(request.ChangePasswordRequest.NewPassword, oldhashers))
                    return Error.Conflict(description: ".لا يمكنك استخدام احدي كلمات المرور السابقة");

                user.PasswordHash = newpasword.Value;
                //await _userRepository.Update(user);
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
