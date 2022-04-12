using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.OverViews;

public interface IReadOverViewRepository
{
    Task<IReadOnlyCollection<OverView>> GetOverViewListAsync();
    Task<OverView> GetByIdOverViewAsync(Guid id);
}
