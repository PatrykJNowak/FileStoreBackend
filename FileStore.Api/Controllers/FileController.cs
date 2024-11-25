using FileStore.Api.UseCases.DeleteFile;
using FileStore.Api.UseCases.GetFile;
using FileStore.Api.UseCases.GetFileInfo;
using FileStore.Api.UseCases.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]/api")]
public class FileController : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<GetFileInfoDto>>> GetFileInfo(
        [FromServices] IMediator mediator,
        CancellationToken ct)
    {
        var file = await mediator.Send(new GetFileInfoQuery(), ct);

        return file;
    } 
    
    [HttpGet("{fileId}")]
    public async Task<ActionResult> Get(
        [FromServices] IMediator mediator,
        [FromRoute] Guid fileId,
        CancellationToken ct)
    {
        var file = await mediator.Send(new GetFileQuery()
        {
            FileId = fileId
        }, ct);

        return Ok(File(file.Stream, "application/octet-stream", file.FileName, enableRangeProcessing: true));
    }

    [HttpPost]
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
    
    [HttpDelete("{fileId}")]
    [RequestSizeLimit(long.MaxValue)]
    public async Task<ActionResult> Delete(
        [FromServices] IMediator mediator,
        [FromRoute] Guid fileId,
        CancellationToken ct)
    {
        await mediator.Send(new DeleteFileCommand()
        {
            FileId = fileId
        }, ct);

        return Ok();
    }
}