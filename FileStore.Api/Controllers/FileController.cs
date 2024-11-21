using FileStore.Api.UseCases.GetFile;
using FileStore.Api.UseCases.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.Controllers;

[ApiController]
[Route("[controller]/api")]
public class FileController : ControllerBase
{
    [HttpGet("{fileId}")]
    public async Task<ActionResult<MemoryStream>> Get(
        [FromServices] IMediator mediator,
        [FromRoute] Guid fileId,
        CancellationToken ct)
    {
        var file = await mediator.Send(new GetFileQuery()
        {
            FileId = fileId
        }, ct);

        var resultFile = File(file.Stream, "application/octet-stream", file.FileName);
        resultFile.EnableRangeProcessing = true;

        return resultFile;
    }

    [HttpPost("upload")]
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