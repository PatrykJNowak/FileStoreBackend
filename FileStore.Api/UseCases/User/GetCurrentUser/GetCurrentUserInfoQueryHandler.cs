using FileStore.Domain.Interfaces;
using MediatR;

namespace FileStore.Api.UseCases.User.GetCurrentUser;

public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetCurrentUserInfoQuery, GetCurrentUserInfoDto>
{
    private readonly ICurrentUser _currentUser;

    public GetCurrentUserInfoQueryHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public async Task<GetCurrentUserInfoDto> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        return new GetCurrentUserInfoDto()
        {
            UserId = Guid.Parse(_currentUser.UserId),
            UserName = _currentUser.UserName!
        };
    }
}