using OGWeb.Core.Interfaces.Repositories.Videos;
using OGWeb.Core.Interfaces.Repositories.Works;

namespace OGWeb.Core.Interfaces.Repositories;

public interface IReadRepositoryManager
{
    public IReadWorkRepository WorkRepository { get; }
    public IReadVideoRepository VideoRepository { get; }
}