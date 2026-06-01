using IndexMe.Application.Features.Links.Commands.ChangeDisplayOrder;
using IndexMe.Application.Features.Links.Commands.ChangeTitle;
using IndexMe.Application.Features.Links.Commands.ChangeUrl;
using IndexMe.Application.Features.Links.Commands.CreateLink;
using IndexMe.Application.Features.Links.Queries.TrackLink;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.MediatR;

namespace IndexMe.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LinksController(ISender sender) : ControllerBase
{

    [HttpGet("track-link")]
    [AllowAnonymous]
    public async Task<IActionResult> TrackLink([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new TrackClickQuery(id), cancellationToken);

        if (result.IsSuccess) return Ok(result.Data);
        else return BadRequest(result.Error);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateLinkCommand request, CancellationToken cancellationToken)
    {
        return BadRequest("Şuanda bu işleme izin verilmemektedir | This process is not allowed right now");
    }

    [HttpPatch("change-url")]
    public async Task<IActionResult> ChangeUrl(ChangeUrlCommand request, CancellationToken cancellationToken)
    {
        return BadRequest("Şuanda bu işleme izin verilmemektedir | This process is not allowed right now");
    }

    [HttpPatch("change-title")]
    public async Task<IActionResult> ChangeTitle(ChangeTitleCommand request, CancellationToken cancellationToken)
    {
        return BadRequest("Şuanda bu işleme izin verilmemektedir | This process is not allowed right now");
    }

    [HttpPatch("change-display-order")]
    public async Task<IActionResult> ChangeDisplayOrder(ChangeDisplayOrderCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        if (result.IsSuccess) return Ok();
        else return BadRequest(result.Error);
    }
}
