using BCrypt.Net;
using ErrorOr;

namespace Project.Application.Authentication.Dtos
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
