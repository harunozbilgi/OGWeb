using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.Works;
using OGWeb.Infrastructure.Context;
using OGWeb.Infrastructure.Helpers;

namespace OGWeb.Infrastructure.Repositories.Works;

public class WriteWorkRepository : IWriteWorkRepository
{
    private readonly ApplicationDbContext _context;

    public WriteWorkRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Work> CreateWorkAsync(Work work)
    {
        work.CreatedDate = DateTime.UtcNow;

        work.SlugUrl = UrlSeoHelper.UrlSeo(work.Title);

        var result = _context.Works.Add(work);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<bool> CreateWorkFileAsync(WorkFile work)
    {
        _context.WorkFiles.Add(work);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveWorkAsync(Guid workId)
    {
        var result = await _context.Works.FirstOrDefaultAsync(x => x.Id == workId);

        if (result == null) throw new ArgumentNullException(nameof(result));

        _context.Works.Remove(result);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveWorkFileAsync(Guid workId)
    {
        var result = await _context.WorkFiles.FirstOrDefaultAsync(x => x.Id == workId);

        if (result == null) throw new ArgumentNullException(nameof(result));

        _context.WorkFiles.Remove(result);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateWorkAsync(Work work)
    {
        work.SlugUrl = UrlSeoHelper.UrlSeo(work.Title);

        _context.Works.Update(work);

        await _context.SaveChangesAsync();

        return true;
    }
}