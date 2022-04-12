using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.AppSettings;

public interface IWriteAppSettingRepository
{
    Task<AppSetting> AppSettingAsync(AppSetting appSetting);
}