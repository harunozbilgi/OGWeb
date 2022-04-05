
using System.Text.Json.Serialization;

namespace OGWeb.Core.Wrappers;

public class CustomResponse<T>
{
    public T Data { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; }
    public static CustomResponse<T> Success(T data, int statusCode)
    {
        return new CustomResponse<T> { Data = data, StatusCode = statusCode };
    }
    public static CustomResponse<T> Success(int statusCode)
    {
        return new CustomResponse<T> { StatusCode = statusCode };
    }
    public static CustomResponse<T> Fail(List<string> errors, int statusCode)
    {
        return new CustomResponse<T> { Errors = errors, StatusCode = statusCode };
    }
    public static CustomResponse<T> Fail(string error, int statusCode)
    {
        return new CustomResponse<T> { Errors = new List<string> { error }, StatusCode = statusCode };
    }
}
