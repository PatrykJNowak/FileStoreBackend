using FileStore.Api.UseCases.User.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("GetUser")]
    public async Task<ActionResult<GetCurrentUserInfoDto>> GetCurrentUserInfo(
        [FromServices] IMediator mediator,
        CancellationToken ct)
    {
        return Ok(await mediator.Send(new GetCurrentUserInfoQuery(), ct));
    }
}