using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.Videos;

public interface IWriteVideoRepository
{
    Task<bool> CreateVideoAsync(Video video);
    Task<bool> UpdateVideoAsync(Video video);
    Task<bool> RemoveVideoAsync(Guid videoId);
}