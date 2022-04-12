using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.OverViews;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.OverViews;

public class ReadOverViewRepository : IReadOverViewRepository
{
    private readonly ApplicationDbContext _context;

    public ReadOverViewRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<OverView> GetByIdOverViewAsync(Guid id)
    {
        return await _context.OverViews.FindAsync(id);
    }

    public async Task<IReadOnlyCollection<OverView>> GetOverViewListAsync()
    {
        return await _context.OverViews.AsNoTracking().ToListAsync();
    }
}
