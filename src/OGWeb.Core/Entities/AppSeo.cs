namespace OGWeb.Core.Entities;

public class AppSeo : BaseEntity
{
    public string Page { get; set; }
    public string Title { get; set; }
    public string Keyword { get; set; }
    public string Description { get; set; }
    public bool? IsStatic { get; set; }
    public bool? IsDynamic { get; set; }
}
