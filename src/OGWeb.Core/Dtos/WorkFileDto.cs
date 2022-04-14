using System.Text.Json.Serialization;

namespace OGWeb.Core.Dtos;

public class WorkFileDto
{
    [JsonIgnore]
    public string ImageUrl { get; set; }
    public string Image_Url { get; set; }
}
