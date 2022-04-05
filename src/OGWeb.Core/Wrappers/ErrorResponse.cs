using System.Text.Json;

namespace OGWeb.Core.Wrappers;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
