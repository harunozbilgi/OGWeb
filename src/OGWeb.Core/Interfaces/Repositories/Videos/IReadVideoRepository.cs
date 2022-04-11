using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.Videos;

public interface IReadVideoRepository
{
    Task<IReadOnlyCollection<Video>> GetVideoListAsync();
    Task<Video> GetByIdVideoAsync(Guid id);
}