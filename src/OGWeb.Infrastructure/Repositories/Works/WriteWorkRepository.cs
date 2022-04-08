using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.Works;
using OGWeb.Infrastructure.Context;

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
        var result = _context.Works.Add(work);

        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<bool> RemoveWorkAsync(Guid workId)
    {
        var result = await _context.Works.FirstOrDefaultAsync(x => x.Id == workId);

        if (result == null) throw new Exception("not data");

        _context.Works.Remove(result);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateWorkAsync(Work work)
    {
        _context.Works.Update(work);

        await _context.SaveChangesAsync();

        return true;
    }
}