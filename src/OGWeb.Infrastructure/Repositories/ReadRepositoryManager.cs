using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Interfaces.Repositories.Videos;
using OGWeb.Core.Interfaces.Repositories.Works;
using OGWeb.Infrastructure.Context;
using OGWeb.Infrastructure.Repositories.Videos;
using OGWeb.Infrastructure.Repositories.Works;

namespace OGWeb.Infrastructure.Repositories;

public class ReadRepositoryManager : IReadRepositoryManager
{
    private readonly Lazy<IReadWorkRepository> _readWorkRepository;
    private readonly Lazy<IReadVideoRepository> _readVideoRepository;

    public ReadRepositoryManager(ApplicationDbContext context)
    {
        _readWorkRepository = new Lazy<IReadWorkRepository>(() => new ReadWorkRepository(context));
        _readVideoRepository = new Lazy<IReadVideoRepository>(() => new ReadVideoRepository(context));
    }

    public IReadWorkRepository WorkRepository => _readWorkRepository.Value;
    public IReadVideoRepository VideoRepository => _readVideoRepository.Value;
}