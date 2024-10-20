using MediatR;

namespace FileStore.Api.UseCases;

public class TestRequest : IRequest
{
    public int TestValue { get; set; }
}