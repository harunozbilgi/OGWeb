using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.AppSeos;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.AppSeos;

public class ReadAppSeoRepository : IReadAppSeoRepository
{
    private readonly ApplicationDbContext _context;

    public ReadAppSeoRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyCollection<AppSeo>> GetAppSeoListAsync()
    {
        return await _context.AppSeos.AsNoTracking().ToListAsync();
    }

    public async Task<AppSeo> GetByIdAppSeoAsync(Guid id)
    {
        return await _context.AppSeos.FindAsync(id);
    }

    public async Task<AppSeo> GetByPageAppSeoAsync(string page)
    {
        return await _context.AppSeos.FirstOrDefaultAsync(x => x.Page == page);
    }
}
