using MediatR;

namespace FileStore.Api.UseCases;

public class TestRequestHandler : IRequestHandler<TestRequest>
{
    public async Task Handle(TestRequest request, CancellationToken cancellationToken)
    {
        var a = "test;";
        var b = a.Split();
        var c = b[10];

        return;
    }
}