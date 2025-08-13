using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Notification;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    // [Authorize(Roles ="Admin")]
    public async Task<IActionResult> CreateUser([FromForm] AddUserRequestModel model)
    {
        await _userService.CreateUserAsync(model);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost("filter")]
    [Authorize]
    public async Task<IActionResult> GetUsers([FromBody] UserFilterParam filterParams)
    {
        var result = await _userService.GetUsersAsync(filterParams);

        var response = ApiResponseBuilder.With<PaginatedResult<UserResponseViewModel>>().StatusCode(200).SetData(result).Build();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        UserResponseViewModel user = await _userService.GetByIdAsync(id);

        var response = ApiResponseBuilder.With<UserResponseViewModel>().StatusCode(200).SetData(user).Build();

        return Ok(response);
    }

    [HttpGet("usernamelist")]
    public async Task<IActionResult> GetUserNamesList()
    {
        List<UserNameResponseViewModel> users = await _userService.GetAllUserFullNameList();

        var response = ApiResponseBuilder.With<List<UserNameResponseViewModel>>().SetData(users).Build();

        return Ok(response);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequestModel model)
    {
        await _userService.UpdateUserAsync(model);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost("ChangeNotificationStatus")]
    public async Task<IActionResult> ChangeNotificationStatus(UpdateNotificationStatus request)
    {
        await _userService.ChangeNotificationStatus(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }
}
