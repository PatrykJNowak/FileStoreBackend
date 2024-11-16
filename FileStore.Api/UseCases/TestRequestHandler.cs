using FileStore.Api.DJ.Exception;
using FileStore.Infrastructure.Interfaces;
using MediatR;

namespace FileStore.Api.UseCases;

public class TestRequestHandler : IRequestHandler<TestRequest>
{
    private readonly ICurrentUser _currentUser;

    public TestRequestHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public async Task Handle(TestRequest request, CancellationToken cancellationToken)
    {
        // var user = await _currentUser.GetUserAsync();

        Console.WriteLine("Done");
        
        // throw new UnauthorizedException();
        
        return;
    }
}