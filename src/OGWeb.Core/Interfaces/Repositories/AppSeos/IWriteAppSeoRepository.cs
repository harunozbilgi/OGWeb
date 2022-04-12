using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.AppSeos;

public interface IWriteAppSeoRepository
{
    Task<bool> CreateAppSeoAsync(AppSeo appSeo);
    Task<bool> UpdateAppSeoAsync(AppSeo appSeo);
    Task<bool> RemoveAppSeoAsync(Guid appSeoId);
}
