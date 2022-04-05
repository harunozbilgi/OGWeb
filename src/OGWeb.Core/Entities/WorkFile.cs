namespace OGWeb.Core.Entities;

public class WorkFile : BaseEntity
{
    public Guid WorkId { get; set; }
    public string ImageUrl { get; set; }
    public bool? IsCover { get; set; }
    public virtual Work Work { get; set; }
}
