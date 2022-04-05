namespace OGWeb.Core.Wrappers;

public class PaginatedResponse<T>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
    public IReadOnlyList<T> Data { get; set; }

    public PaginatedResponse(int pageIndex, int pageSize, long count, IReadOnlyList<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }
}
