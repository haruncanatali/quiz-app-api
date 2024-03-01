using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Auth.Queries.HardPasswordChange;
using QuizApp.Application.Auth.Queries.Login;
using QuizApp.Application.Auth.Queries.Login.Dtos;
using QuizApp.Application.Auth.Queries.RefreshToken;
using QuizApp.Application.Common.Models;

namespace QuizApp.Api.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<BaseResponseModel<LoginDto>>> Login([FromBody] LoginCommand loginModel)
    {
        BaseResponseModel<LoginDto> loginResponse = await Mediator.Send(loginModel);
        return Ok(loginResponse);
    }

    [HttpGet]
    [Route("RefreshToken")]
    public async Task<ActionResult<BaseResponseModel<LoginDto>>> RefreshToken(string refreshToken)
    {
        return Ok(await Mediator.Send(new RefreshTokenCommand { RefreshToken = refreshToken }));
    }
    
    [Authorize]
    [HttpPost]
    [Route("HardPasswordChange")]
    public async Task<IActionResult> HardPasswordChange(HardPasswordChangeCommand request)
    {
        await Mediator.Send(request);
        return Ok();
    }
}