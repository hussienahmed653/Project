using ErrorOr;
using Project.Application.Authentication.Common;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.Mapping.Authentications;
using Project.Domain.Common.Interfaces;

namespace Project.Application.Authentication.Queries
{
    public class LoginQuerieHandler : IRequestHandlerRepository<LoginQuerie, ErrorOr<AuthReseult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQuerieHandler(IUnitOfWork unitOfWork,
                                  IPasswordHasher passwordHasher,
                                  IUserRepository userRepository,
                                  IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthReseult>> Handle(LoginQuerie request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if(!await _userRepository.ExistByEmail(request.LoginRequest.Email))
                    return Error.Conflict(description: ".بيانات تسجيل دخولك لا تتناسب مع اي حساب في سجلاتنا");

                var user = await _userRepository.GetUserByEmail(request.LoginRequest.Email);

                if (!_passwordHasher.IsCorrectPassword(request.LoginRequest.password, user.PasswordHash))
                    return Error.Unauthorized(description: ".بيانات تسجيل دخولك لا تتناسب مع اي حساب في سجلاتنا");

                await _unitOfWork.CommitAsync();
                var maptouser = user.MapToAuthResult();
                maptouser.Token = _jwtTokenGenerator.GenerateToken(user);
                return maptouser;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("");
            }
        }
    }
}
