using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.Sliders;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.Sliders;

public class ReadSliderRepository : IReadSliderRepository
{
    private readonly ApplicationDbContext _context;

    public ReadSliderRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Slider> GetByIdSliderAsync(Guid id)
    {
        return await _context.Sliders.FindAsync(id);
    }

    public async Task<IReadOnlyCollection<Slider>> GetSliderListAsync()
    {
        return await _context.Sliders.AsNoTracking().ToListAsync();
    }
}