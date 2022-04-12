using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.OverViews;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.OverViews;

public class WriteOverViewRepository : IWriteOverViewRepository
{
    private readonly ApplicationDbContext _context;

    public WriteOverViewRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> CreateOverViewAsync(OverView overView)
    {
        _context.OverViews.Add(overView);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveOverViewAsync(Guid overViewId)
    {
        var result = await _context.OverViews.FirstOrDefaultAsync(x => x.Id == overViewId);

        if (result == null) throw new ArgumentNullException(nameof(result));

        _context.OverViews.Remove(result);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateOverViewAsync(OverView overView)
    {
        _context.OverViews.Update(overView);
        await _context.SaveChangesAsync();
        return true;
    }
}
