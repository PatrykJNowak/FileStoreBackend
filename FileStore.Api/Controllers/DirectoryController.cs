using FileStore.Api.UseCases.Directory.Create;
using FileStore.Api.UseCases.Directory.Delete;
using FileStore.Api.UseCases.Directory.GetCurrentUserView;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]/api")]
public class DirectoryController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GetCurrentUserViewDto>> GetCurrentUserView(
        [FromServices] IMediator mediator,
        Guid? directoryId,
        CancellationToken ct)
    {
        var response = await mediator.Send(new GetCurrentUserViewQuery()
        {
            DirectoryId = directoryId
        }, ct);

        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] IMediator mediator,
        [FromBody] CreateDirectoryCommand command,
        CancellationToken ct)
    {
        command ??= new();
        var directoryId = await mediator.Send(command, ct);

        return Ok(directoryId);
    }
    
    [HttpDelete("{directoryId}")]
    public async Task<ActionResult> Delete(
        [FromServices] IMediator mediator,
        [FromRoute] Guid directoryId,
        CancellationToken ct)
    {
        await mediator.Send(new DeleteDirectoryCommand()
        {
            DirectoryId = directoryId
        }, ct);

        return Ok();
    }

    // move?
    
    // rename (patch)
}