namespace IplAuction.Entities.DTOs;

public static class ApiResponseBuilder 
{
    public static ApiResponse<object> Create(int statusCode)
    {
        return new ApiResponse<object>
        {
            StatusCode = statusCode,
            Data = null
        };
    }

    public static ApiResponse<object> Create(int statusCode, string message)
    {
        return new ApiResponse<object>
        {
            StatusCode = statusCode,
            Message = message,
            Data = null
        };
    }

    public static ApiResponse<object> CreateNotFoundResponse(string entity)
    {
        return new ApiResponse<object>
        {
            StatusCode = 404,
            Message = string.Format(Messages.NotFound, entity),
            Data = null
        };
    }

    public static ApiResponseBuilder<T> With<T>() where T : class, new()
    {
        return new ApiResponseBuilder<T>();
    }
}

public class ApiResponseBuilder<T> where T : class, new()
{
    private readonly ApiResponse<T> _response = new();

    public ApiResponseBuilder<T> StatusCode(int code)
    {
        _response.StatusCode = code;
        return this;
    }

    public ApiResponseBuilder<T> Message(string message)
    {
        _response.Message = message;
        return this;
    }

    public ApiResponseBuilder<T> SetData(T? data)
    {
        _response.Data = data;
        return this;
    }

    public ApiResponseBuilder<T> SetNotFoundResponse(string entity)
    {
        _response.StatusCode = 404;
        _response.Message = string.Format(Messages.NotFound, entity);
        return this;
    }

    public ApiResponseBuilder<T> SetStatusCodeAndMessage(int statusCode, string message)
    {
        _response.StatusCode = statusCode;
        _response.Message = message;
        return this;
    }

    public ApiResponse<T> Build() => _response;
}

public class ApiResponse<T> where T : class, new()
{
    public ApiResponse() { }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public int StatusCode { get; set; }
}
 