using ErrorOr;

namespace Project.Application.Authentication.Dtos
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }


        public static ErrorOr<Success> PasswordNoMatched(string newpassword, string confirmpassword)
        {
            if(newpassword != confirmpassword)
                return Error.Conflict(description: ".كلمة المرور الجديدة غير متطابقة");

            return Result.Success;
        }
    }
}
