namespace OGWeb.Core.Dtos;

public class WorkDto
{
    public Guid Id { get; set; }
    public string AppSeoCode { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SlugUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool? IsActived { get; set; }
}