using FluentValidation;

namespace FileStore.Api.UseCases;

public class TestRequestValidator : AbstractValidator<TestRequest>
{
    public TestRequestValidator()
    {
        RuleFor(x => x.TestValue).GreaterThanOrEqualTo(4);
    }
}