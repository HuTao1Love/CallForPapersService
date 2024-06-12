using System.Net;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationsController(IApplicationService applicationService) : ControllerBase
{
    private CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<ApplicationDto>> CreateApplication([FromBody] ApplicationNoIdDto applicationDto)
    {
        return await applicationService.CreateAsync(applicationDto, CancellationToken);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<ApplicationDto>> EditApplication(
        Guid id,
        [FromBody] ApplicationNoIdDto applicationDto)
    {
        return await applicationService.UpdateAsync(id, applicationDto, CancellationToken);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteApplication(Guid id)
    {
        await applicationService.DeleteAsync(id, CancellationToken);
        return Ok();
    }

    [HttpPost("{id:Guid}/submit")]
    public async Task<ActionResult> SubmitApplication(Guid id)
    {
        await applicationService.SubmitAsync(id, CancellationToken);
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationDto>> GetApplicationById(Guid id)
    {
        var applicationDto = await applicationService.FindByIdAsync(id, CancellationToken);

        if (applicationDto is null)
        {
            return Problem($"Not found application with ID {id}", statusCode: (int)HttpStatusCode.NotFound);
        }

        return applicationDto;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetApplications(
        [FromQuery(Name = "submittedAfter")] string? submittedAfterString,
        [FromQuery(Name = "unsubmittedOlder")] string? unsubmittedOlderString)
    {
        return Ok(
            await applicationService.GetApplicationsAsync(
                submittedAfterString,
                unsubmittedOlderString,
                CancellationToken));
    }
}