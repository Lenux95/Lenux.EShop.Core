namespace Catalog.API.Models.Api;

public class ApiResponse
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;

    public static ApiResponse Success(string message = "success")
        => new() { Code = 200, Message = message };

    public static ApiResponse Fail(int code, string message)
        => new() { Code = code, Message = message };
}

public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string message = "success")
        => new() { Code = 200, Message = message, Data = data };

    public static ApiResponse<T> Fail(int code, string message)
        => new() { Code = code, Message = message, Data = default };
}
