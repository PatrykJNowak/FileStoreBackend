using FileStore.Api.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("{testValue}")]
    public async Task<IActionResult> Get([FromServices] IMediator mediator,
        [FromRoute] int testValue,
        CancellationToken ct)
    {
        await mediator.Send(new TestRequest()
        {
            TestValue = testValue
        }, ct);

        return Ok();
    }
}