using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.AppSettings;

public interface IReadAppSettingRepository
{
    Task<AppSetting> GetAppSettingByAsync();
}