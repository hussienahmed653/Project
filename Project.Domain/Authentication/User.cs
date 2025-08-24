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
    }
}
