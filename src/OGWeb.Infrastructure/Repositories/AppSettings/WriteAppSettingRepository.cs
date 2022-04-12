using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.AppSettings;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.AppSettings;

public class WriteAppSettingRepository : IWriteAppSettingRepository
{
    private readonly ApplicationDbContext _context;

    public WriteAppSettingRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<AppSetting> AppSettingAsync(AppSetting appSetting)
    {
        var result = await _context.AppSettings.FirstOrDefaultAsync();
        if (result == null)
        {
            var response = _context.AppSettings.Add(appSetting);
            await _context.SaveChangesAsync();
            return response.Entity;
        }
        else
        {
            appSetting.Id = result.Id;
            _context.AppSettings.Update(appSetting);
            await _context.SaveChangesAsync();
        }

        return result;
    }
}