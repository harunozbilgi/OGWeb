using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.Sliders;

public interface IReadSliderRepository
{
    Task<IReadOnlyCollection<Slider>> GetSliderListAsync();
    Task<Slider> GetByIdSliderAsync(Guid id);
}