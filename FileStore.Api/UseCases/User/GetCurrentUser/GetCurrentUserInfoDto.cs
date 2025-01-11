namespace FileStore.Api.UseCases.User.GetCurrentUser;

public class GetCurrentUserInfoDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}