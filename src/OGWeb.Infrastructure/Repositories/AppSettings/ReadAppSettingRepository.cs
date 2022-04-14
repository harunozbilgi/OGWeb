using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.AppSettings;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.AppSettings;

public class ReadAppSettingRepository : IReadAppSettingRepository
{
    private readonly ApplicationDbContext _context;

    public ReadAppSettingRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<AppSetting> GetAppSettingByAsync()
    {
        var result = await _context.AppSettings.FirstOrDefaultAsync();
        return _context.AppSettings.Any() ? result : new AppSetting();
    }

}