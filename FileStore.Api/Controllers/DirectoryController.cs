using FileStore.Api.UseCases.Directory.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]/api")]
public class DirectoryController : ControllerBase
{
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