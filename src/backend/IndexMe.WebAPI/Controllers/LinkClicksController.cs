using IndexMe.Application.Features.LinkClicks.Queries.GetClicksByLinkId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.MediatR;

namespace IndexMe.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LinkClicksController(ISender sender) : ControllerBase
{
    [HttpGet("get-link-clicks")]
    public async Task<IActionResult> GetLinkClicks([FromQuery] Guid linkId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetClicksByLinkIdQuery(linkId), cancellationToken);

        if (result.IsSuccess) return Ok(result.Data);
        else return BadRequest(result.Error);
    }
}
