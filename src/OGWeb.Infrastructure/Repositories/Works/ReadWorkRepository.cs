using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.Works;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.Works;

public class ReadWorkRepository : IReadWorkRepository
{
    private readonly ApplicationDbContext _context;
    public ReadWorkRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Work> GetByIdWorkAsync(Guid id)
    {
        var result = await _context.Works.FindAsync(id);
        return result;
    }

    public async Task<IReadOnlyCollection<Work>> GetWorkListAsync()
    {
        var result = await _context.Works.AsNoTracking().OrderByDescending(o => o.CreatedDate).Include(x => x.WorkFiles).ToListAsync();
        return result;
    }
}