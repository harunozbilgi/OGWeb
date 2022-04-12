using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.Videos;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.Videos;

public class ReadVideoRepository : IReadVideoRepository
{
    private readonly ApplicationDbContext _context;

    public ReadVideoRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Video> GetByIdVideoAsync(Guid id)
    {
        return await _context.Videos.FindAsync(id);
    }

    public async Task<IReadOnlyCollection<Video>> GetVideoListAsync()
    {
        return await _context.Videos.AsNoTracking().ToListAsync();
    }
}