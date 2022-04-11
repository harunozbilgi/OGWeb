namespace OGWeb.Core.Entities;

public class Work : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string SlugUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool? IsActived { get; set; }
    public string Keyword_Seo { get; set; }
    public string Description_Seo { get; set; }
    public virtual ICollection<WorkFile> WorkFiles { get; set; }

}
