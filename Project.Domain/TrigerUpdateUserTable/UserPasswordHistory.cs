namespace Project.Domain.TrigerUpdateUserTable
{
    public class UserPasswordHistory
    {
        public Guid UserGuid { get; set; }
        public string OldPassword { get; set; }
    }
}
