using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("All")]
    public async Task<IActionResult> GetAllUsers()
    {
        List<UserResponseViewModel> users = await _userService.GetAllAsync();

        var response = ApiResponseBuilder.With<List<UserResponseViewModel>>().StatusCode(200).SetData(users).Build();

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserFilterParam filterParams)
    {
        var result = await _userService.GetUsersAsync(filterParams);
        return Ok(result);
    }


    // [HttpGet]
    // public async Task<IActionResult> GetAllUsers([FromQuery] PaginationParams paginationParams)
    // {
    //     PaginatedResult<User> users = await _userService.GetPaginated(paginationParams);

    //     var response = ApiResponseBuilder.With<PaginatedResult<User>>().StatusCode(200).SetData(users).Build();

    //     return Ok(response);
    // }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        UserResponseViewModel user = await _userService.GetByIdAsync(id);

        var response = ApiResponseBuilder.With<UserResponseViewModel>().StatusCode(200).SetData(user).Build();

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestModel model)
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
}
