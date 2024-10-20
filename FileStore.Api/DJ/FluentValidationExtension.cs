using System.Net;
using FluentValidation;

namespace FileStore.Api.DJ;

public static class FluentValidationExtension
{
    public static IRuleBuilderOptions<T, TProperty> WithValidationError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, HttpStatusCode errorCode, string? message = null)
    {
        rule.WithErrorCode(errorCode.ToString())
            .WithMessage(message);

        return rule;
    }
    
    public static IRuleBuilderOptions<T, TProperty> WithValidationError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, string? message = null)
    {
        rule.WithErrorCode(HttpStatusCode.BadRequest.ToString())
            .WithMessage(message);

        return rule;
    }
}