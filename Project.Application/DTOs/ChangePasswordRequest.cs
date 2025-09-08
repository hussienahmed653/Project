using BCrypt.Net;
using ErrorOr;

namespace Project.Application.DTOs
{
    public class ChangePasswordRequest
    {
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
