using FileStore.Api.UseCases.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.Controllers;

[ApiController]
[Route("api")]
public class FileController : ControllerBase
{
    [HttpPost("test/route")]
    [RequestSizeLimit(long.MaxValue)]
    public async Task<ActionResult> Upload(
        [FromServices] IMediator mediator,
        IFormFile file,
        CancellationToken ct)
    {
        await mediator.Send(new UploadFileCommand()
        {
            File = file
        }, ct);
    
        return Ok();
    }
}