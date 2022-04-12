using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.AppSeos;

public interface IReadAppSeoRepository
{
    Task<IReadOnlyCollection<AppSeo>> GetAppSeoListAsync();
    Task<AppSeo> GetByIdAppSeoAsync(Guid id);
    Task<AppSeo> GetByPageAppSeoAsync(string page);
}
