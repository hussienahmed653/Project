using ErrorOr;
using Project.Application.Authentication.Dtos;
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

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork,
                                            IUserRepository userRepository,
                                            IPasswordHasher passwordHasher,
                                            IUserPasswordHistorieRepository passwordHistorieRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _passwordHistorieRepository = passwordHistorieRepository;
        }

        public async Task<ErrorOr<Updated>> Handle(ChangePasswordCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if(!await _userRepository.ExistByEmail(request.ChangePasswordRequest.Email))
                    return Error.Conflict(description: ".بيانات تسجيل دخولك لا تتناسب مع اي حساب في سجلاتنا");

                var user = await _userRepository.GetUserByEmail(request.ChangePasswordRequest.Email);

                if(!_passwordHasher.IsCorrectPassword(request.ChangePasswordRequest.CurrentPassword, user.PasswordHash))
                    return Error.Conflict(description: ".بيانات تسجيل دخولك لا تتناسب مع اي حساب في سجلاتنا");

                var differentpasswords = User.CurrentPasswordIsEqualsNewPassword(request.ChangePasswordRequest.CurrentPassword, request.ChangePasswordRequest.NewPassword);

                if(differentpasswords.IsError)
                    return differentpasswords.Errors;

                var passwordmatch = User.PasswordNoMatched(request.ChangePasswordRequest.NewPassword, request.ChangePasswordRequest.ConfirmNewPassword);
                if (passwordmatch.IsError)
                    return passwordmatch.Errors;
                var newpasword = _passwordHasher.HashPassword(request.ChangePasswordRequest.NewPassword);
                if(newpasword.IsError)
                    return newpasword.Errors;

                var oldhashers = await _passwordHistorieRepository.ExistByPasswordHash(user.Id);

                if(!User.CanUseThisPassword(request.ChangePasswordRequest.NewPassword, oldhashers))
                    return Error.Conflict(description: ".لا يمكنك استخدام احدي كلمات المرور السابقة");

                user.PasswordHash = newpasword.Value;
                await _userRepository.Update(user);
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
