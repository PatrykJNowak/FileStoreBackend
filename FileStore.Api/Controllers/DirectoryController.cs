using FileStore.Api.UseCases.Directory.Create;
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
    public async Task<ActionResult> Create(
        [FromServices] IMediator mediator,
        [FromBody] CreateDirectoryCommand command,
        CancellationToken ct)
    {
        command ??= new();
        await mediator.Send(command, ct);

        return Ok();
    }
    
    
    
    // get file from 
    
    // delete
    
    // move?
    
    // rename (patch)
}