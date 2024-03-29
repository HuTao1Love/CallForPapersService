using Abstractions.Services;
using CallForPapersService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CallForPapersService.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IApplicationService applicationService) : ControllerBase
{
    [HttpGet("{authorId:Guid}/currentapplication")]
    public async Task<ActionResult<ApplicationDto>> GetCurrentApplication(Guid authorId)
    {
        var applicationDto = await applicationService.GetCurrentApplicationAsync(authorId);
        if (applicationDto is null) throw new NotFoundException($"Not found current application of author {authorId}");
        return applicationDto;
    }
}