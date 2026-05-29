using IndexMe.Application.Features.Users.Commands.ChangeBio;
using IndexMe.Application.Features.Users.Commands.ChangeDisplayName;
using IndexMe.Application.Features.Users.Commands.ChangeEmail;
using IndexMe.Application.Features.Users.Commands.ChangePassword;
using IndexMe.Application.Features.Users.Commands.LoginUser;
using IndexMe.Application.Features.Users.Commands.RegisterUser;
using IndexMe.Application.Features.Users.Queries.GetMyInfo;
using IndexMe.Application.Features.Users.Queries.GetUserInfoByUsername;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.MediatR;

namespace IndexMe.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpGet("get-my-info")]
    public async Task<IActionResult> GetMyInfo(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyInfoQuery(), cancellationToken);

        if (result.IsSuccess) return Ok(result.Data);
        else return BadRequest(result.Error);
    }

    [HttpGet("get-user-info")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserInfo([FromQuery] string username, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetUserInfoByUsernameQuery(username), cancellationToken);

        if (result.IsSuccess) return Ok(result.Data);
        else return BadRequest(result.Error);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        if (result.IsSuccess) return Ok();
        else return BadRequest(result.Error);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        if (result.IsSuccess) return Ok(result.Data);
        else return BadRequest(result.Error);
    }

    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        if (result.IsSuccess) return Ok();
        else return BadRequest(result.Error);
    }

    [HttpPatch("change-email")]
    public async Task<IActionResult> ChangeEmail(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        if (result.IsSuccess) return Ok();
        else return BadRequest(result.Error);
    }

    [HttpPatch("change-display-name")]
    public async Task<IActionResult> ChangeDisplayName(ChangeDisplayNameCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        if (result.IsSuccess) return Ok();
        else return BadRequest(result.Error);
    }

    [HttpPatch("change-bio")]
    public async Task<IActionResult> ChangeBio(ChangeBioCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        if (result.IsSuccess) return Ok();
        else return BadRequest(result.Error);
    }
}
