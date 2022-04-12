using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.Sliders;

public interface IWriteSliderRepository
{
    Task<bool> CreateSlidersync(Slider slider);
    Task<bool> UpdateSliderAsync(Slider slider);
    Task<bool> RemoveSliderAsync(Guid sliderId);
}