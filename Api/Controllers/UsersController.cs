using System.Net;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IApplicationService applicationService) : ControllerBase
{
    private CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpGet("{authorId:Guid}/currentapplication")]
    public async Task<ActionResult<ApplicationDto>> GetCurrentApplication(Guid authorId)
    {
        var applicationDto = await applicationService.FindNotSubmittedApplicationAsync(authorId, CancellationToken);

        return applicationDto is not null
            ? Ok(applicationDto)
            : Problem(
                detail: $"Not found current application of author {authorId}",
                statusCode: (int)HttpStatusCode.NotFound);
    }
}