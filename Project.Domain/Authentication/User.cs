using ErrorOr;

namespace Project.Domain.Authentication
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        //public DateTime? LastLoginOn { get; set; }
        //public bool IsActive { get; set; } = true;

        public Roles Role { get; set; } = Roles.User;

        public static ErrorOr<Success> PasswordNoMatched(string newpassword, string confirmpassword)
        {
            if (newpassword != confirmpassword)
                return Error.Conflict(description: ".كلمة المرور الجديدة غير متطابقة");

            return Result.Success;
        }

        public static ErrorOr<Success> CurrentPasswordIsEqualsNewPassword(string currentpassword, string newpassword)
        {
            if (currentpassword == newpassword)
                return Error.Conflict(description: "كلمة المرور الجديدة يجب ان تكون مختلفة عن الحالية");
            return Result.Success;
        }

        public static bool CanUseThisPassword(string newpassword, List<string> oldpasswordhasher)
        {
            foreach (var oldhash in oldpasswordhasher)
            {
                if (BCrypt.Net.BCrypt.EnhancedVerify(newpassword, oldhash))
                    return false;
            }
            return true;
        }
    }
}
