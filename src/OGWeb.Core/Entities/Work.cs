namespace OGWeb.Core.Entities;

public class Work : BaseEntity
{
    public string AppSeoCode { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SlugUrl { get; set; } 
    public DateTime CreatedDate { get; set; }
    public bool? IsActived { get; set; }
    public virtual ICollection<WorkFile> WorkFiles { get; set; }
    
}
