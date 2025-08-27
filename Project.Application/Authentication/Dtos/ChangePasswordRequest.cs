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


        public static ErrorOr<Success> PasswordNoMatched(string newpassword, string confirmpassword)
        {
            if(newpassword != confirmpassword)
                return Error.Conflict(description: ".كلمة المرور الجديدة غير متطابقة");

            return Result.Success;
        }

        public static ErrorOr<Success> CurrentPasswordIsEqualsNewPassword(string currentpassword, string newpassword)
        {
            if(currentpassword == newpassword)
                return Error.Conflict(description: "كلمة المرور الجديدة يجب ان تكون مختلفة عن الحالية");
            return Result.Success;
        }

        public static bool CanUseThisPassword(string newpassword, List<string> oldpasswordhasher)
        {
            foreach(var oldhash in oldpasswordhasher)
            {
                if(BCrypt.Net.BCrypt.EnhancedVerify(newpassword, oldhash))
                    return false;
            }
            return true;
        }
    }
}
