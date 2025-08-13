using System.Text.Json;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IplAuction.Service;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // continue to next middleware
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = 404;
            await WriteResponse(context, ex.Message, 404);
        }
        catch (BadRequestException ex)
        {
            context.Response.StatusCode = 400;
            await WriteResponse(context, ex.Message, 400);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await WriteResponse(context, ex.Message, 500);
        }
    }

    private static Task WriteResponse(HttpContext context, string message, int statusCode)
    {
        context.Response.ContentType = "application/json";

        var response = ApiResponseBuilder.Create(statusCode, message);

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
