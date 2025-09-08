namespace Project.Application.Common.Models
{
    public record CurrentUser
    (
        Guid userId,
        string email,
        string role
    );
}
