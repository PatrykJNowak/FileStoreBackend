using FileStore.Api.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("{testValue}")]
    public async Task<IActionResult> Get([FromServices] IMediator mediator,
        [FromRoute] int testValue,
        CancellationToken ct)
    {
        await mediator.Send(new TestRequest()
        {
            TestValue = testValue
        });

        return Ok();
    }
}