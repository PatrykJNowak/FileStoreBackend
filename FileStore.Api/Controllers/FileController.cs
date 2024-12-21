using FileStore.Api.UseCases.File.DeleteFile;
using FileStore.Api.UseCases.File.GetFile;
using FileStore.Api.UseCases.File.GetFileList;
using FileStore.Api.UseCases.File.GetUserUsedSize;
using FileStore.Api.UseCases.File.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]/api")]
public class FileController : ControllerBase
{
    [HttpGet("GetFileList")]
    public async Task<ActionResult<List<GetFileListQuery>>> GetFileList(
        [FromServices] IMediator mediator,
        Guid directoryId,
        CancellationToken ct)
    {
        var response = await mediator.Send(new GetFileListQuery()
        {
            DirectoryId = directoryId,
        }, ct);

        return Ok(response);
    }
    
    [HttpGet("UsedSize")]
    public async Task<ActionResult<GetUserUsedSizeDto>> GetUserUsedSize(
        [FromServices] IMediator mediator,
        CancellationToken ct)
    {
        var response = await mediator.Send(new GetUserUsedSizeQuery(), ct);

        return Ok(response);
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
        [FromQuery] Guid directoryId,
        IFormFile file,
        CancellationToken ct)
    {
        await mediator.Send(new UploadFileCommand()
        {
            File = file,
            DirectoryId = directoryId
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