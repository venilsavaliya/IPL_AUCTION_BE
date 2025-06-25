using IplAuction.Entities.DTOs;
using IplAuction.Entities.DTOs.Auth;
using IplAuction.Entities.ViewModels;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IUserService userService) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        JwtTokensResponseModel tokens = await _authService.LoginAsync(request);

        var response = ApiResponseBuilder.With<JwtTokensResponseModel>().StatusCode(200).SetData(tokens).Build();

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequestModel request)
    {
        await _userService.CreateUserAsync(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        bool success = await _authService.ForgotPassword(request);

        if (success)
        {
            var response = ApiResponseBuilder.Create(200);
            return Ok(response);
        }

        var badResponse = ApiResponseBuilder.Create(400);
        return BadRequest(badResponse);
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        await _authService.ResetPassword(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        JwtTokensResponseModel tokens = await _authService.RefreshTokenAsync(request.Token);

        var response = ApiResponseBuilder.With<JwtTokensResponseModel>().StatusCode(200).SetData(tokens).Build();

        return Ok(response);
    }

    // [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        UserInfoViewModel userInfo = _authService.GetCurrentUser();

        var response = ApiResponseBuilder.With<UserInfoViewModel>().StatusCode(200).SetData(userInfo).Build();

        return Ok(response);
    }
}
