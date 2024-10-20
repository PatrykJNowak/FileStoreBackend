using System.Net;
using FileStore.Api.DJ;
using FluentValidation;

namespace FileStore.Api.UseCases;

public class TestRequestValidator : AbstractValidator<TestRequest>
{
    public TestRequestValidator()
    {
        RuleFor(x => x.TestValue)
            .GreaterThanOrEqualTo(4)
            .WithValidationError("Request is very bad!");
    }
}