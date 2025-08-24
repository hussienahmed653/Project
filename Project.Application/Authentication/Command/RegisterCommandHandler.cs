using ErrorOr;
using MediatR;
using Project.Application.Authentication.Common;
using Project.Application.Common.Interfaces;
using Project.Application.Mapping.Authentications;
using Project.Domain.Common.Interfaces;

namespace Project.Application.Authentication.Command
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthReseult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterCommandHandler(IUnitOfWork unitOfWork,
                                      IPasswordHasher passwordHasher,
                                      IUserRepository userRepository,
                                      IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthReseult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if (await _userRepository.ExistByEmail(request.Register.Email))
                    return Error.Conflict(description: "User already exists");

                var password = _passwordHasher.HashPassword(request.Register.Password);

                if (password.IsError)
                    return password.Errors;

                //Mapping RegisterRequest to User
                var mapptouser = request.Register.MapToUser();
                mapptouser.PasswordHash = password.Value;

                await _userRepository.Add(mapptouser);
                //Mapping User to AuthResult

                var mapptoauthresult = mapptouser.MapToAuthResult();
                var token = _jwtTokenGenerator.GenerateToken(mapptouser);
                mapptoauthresult.Token = token;
                await _unitOfWork.CommitAsync();
                return mapptoauthresult;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("Error");
            }
        }
    }
}
