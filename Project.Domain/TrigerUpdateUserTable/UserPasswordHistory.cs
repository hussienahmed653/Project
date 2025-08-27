namespace Project.Domain.TrigerUpdateUserTable
{
    public class UserPasswordHistory
    {
        public Guid UserGuid { get; set; }
        public string OldPassword { get; set; }

        /*
         
            create trigger user_Password_Histories
            on Users
            after update
            as
            begin
	            if UPDATE(PasswordHash)
	            begin
		            insert into userPasswordHistories(UserGuid,OldPassword)
		            select d.Id, d.PasswordHash from deleted d
	            end
            end
         
         */
    }
}
