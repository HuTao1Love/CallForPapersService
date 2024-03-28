using Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallForPapersService.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IApplicationService applicationService) : ControllerBase
{
    [HttpGet("{authorId:Guid}/currentapplication")]
    public async Task<ActionResult<ApplicationDto>> GetCurrentApplication(Guid authorId)
    {
        var activityDto = await applicationService.GetCurrentApplicationAsync(authorId);

        return activityDto is not null
            ? activityDto
            : NotFound("User's current application not found");
    }
}