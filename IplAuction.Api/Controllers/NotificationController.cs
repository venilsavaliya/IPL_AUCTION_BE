using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels;
using IplAuction.Entities.ViewModels.Notification;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    private readonly INotificationService _notificationService = notificationService;

    [HttpGet("unread/{userId}")]
    public async Task<IActionResult> GetUnreadNotification(int userId)
    {
        List<NotificationResponse> notifications = await _notificationService.GetUnreadNotification(userId);

        var response = ApiResponseBuilder.With<List<NotificationResponse>>().SetData(notifications).Build();

        return Ok(response);
    }

    [HttpPost("MarkAllNotification/{userId}")]
    public async Task<IActionResult> MarkAllNotificationAsRead(int userId)
    {
        await _notificationService.MarkAllNotificationAsRead(userId);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

 }
