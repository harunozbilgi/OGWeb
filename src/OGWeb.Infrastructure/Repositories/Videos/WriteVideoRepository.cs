using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.Videos;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.Videos;

public class WriteVideoRepository : IWriteVideoRepository
{
    private readonly ApplicationDbContext _context;

    public WriteVideoRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> CreateVideoAsync(Video video)
    {
        _context.Videos.Add(video);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveVideoAsync(Guid videoId)
    {
        var result = await _context.Videos.FirstOrDefaultAsync(x => x.Id == videoId);

        if (result == null) throw new ArgumentNullException(nameof(result));

        _context.Videos.Remove(result);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateVideoAsync(Video video)
    {
        _context.Videos.Update(video);
        await _context.SaveChangesAsync();
        return true;
    }
}