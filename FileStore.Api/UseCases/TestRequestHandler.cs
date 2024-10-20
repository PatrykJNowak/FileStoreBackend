using FileStore.Api.DJ.Exception;
using MediatR;

namespace FileStore.Api.UseCases;

public class TestRequestHandler : IRequestHandler<TestRequest>
{
    public async Task Handle(TestRequest request, CancellationToken cancellationToken)
    {
        throw new UnauthorizeException();
        
        return;
    }
}