using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories.Sliders;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories.Sliders;

public class WriteSliderRepository : IWriteSliderRepository
{
    private readonly ApplicationDbContext _context;

    public WriteSliderRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> CreateSlidersync(Slider slider)
    {
        _context.Sliders.Add(slider);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveSliderAsync(Guid sliderId)
    {
        var result = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == sliderId);

        if (result == null) throw new ArgumentNullException(nameof(result));

        _context.Sliders.Remove(result);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateSliderAsync(Slider slider)
    {
        _context.Sliders.Update(slider);
        await _context.SaveChangesAsync();
        return true;
    }
}