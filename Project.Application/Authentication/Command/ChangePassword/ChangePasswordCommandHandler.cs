using ErrorOr;
using MediatR;
using Project.Application.Authentication.Dtos;
using Project.Application.Common.Interfaces;
using Project.Domain.Common.Interfaces;

namespace Project.Application.Authentication.Command.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<Updated>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork,
                                            IUserRepository userRepository,
                                            IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ErrorOr<Updated>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if(!await _userRepository.ExistByEmail(request.ChangePasswordRequest.Email))
                    return Error.Conflict(description: ".بيانات تسجيل دخولك لا تتناسب مع اي حساب في سجلاتنا");

                var user = await _userRepository.GetUserByEmail(request.ChangePasswordRequest.Email);

                if(!_passwordHasher.IsCorrectPassword(request.ChangePasswordRequest.CurrentPassword, user.PasswordHash))
                    return Error.Conflict(description: ".بيانات تسجيل دخولك لا تتناسب مع اي حساب في سجلاتنا");

                var passwordmatch = ChangePasswordRequest.PasswordNoMatched(request.ChangePasswordRequest.NewPassword, request.ChangePasswordRequest.ConfirmNewPassword);
                if (passwordmatch.IsError)
                    return passwordmatch.Errors;
                var newpasword = _passwordHasher.HashPassword(request.ChangePasswordRequest.NewPassword);
                if(newpasword.IsError)
                    return newpasword.Errors;

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
