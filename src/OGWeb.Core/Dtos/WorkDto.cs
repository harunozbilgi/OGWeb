namespace OGWeb.Core.Dtos;

public class WorkDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SlugUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool? IsActived { get; set; }
    public string Keyword_Seo { get; set; }
    public string Description_Seo { get; set; }
    public List<WorkFileDto> WorkFiles { get; set; }
}