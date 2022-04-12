using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.OverViews;

public interface IWriteOverViewRepository
{
    Task<bool> CreateOverViewAsync(OverView overView);
    Task<bool> UpdateOverViewAsync(OverView overView);
    Task<bool> RemoveOverViewAsync(Guid overViewId);
}
