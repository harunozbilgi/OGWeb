using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.AppSeos;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.AppSeos;

public class WriteAppSeoRepository : IWriteAppSeoRepository
{
    private readonly ApplicationDbContext _context;

    public WriteAppSeoRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> CreateAppSeoAsync(AppSeo appSeo)
    {
        _context.AppSeos.Add(appSeo);
        await _context.SaveChangesAsync();  
        return true;    
    }

    public async Task<bool> RemoveAppSeoAsync(Guid appSeoId)
    {
        var result = await _context.AppSeos.FirstOrDefaultAsync(x => x.Id == appSeoId);

        if (result == null) throw new ArgumentNullException(nameof(result));

        _context.AppSeos.Remove(result);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAppSeoAsync(AppSeo appSeo)
    {
        _context.AppSeos.Update(appSeo);
        await _context.SaveChangesAsync();
        return true;
    }
}
