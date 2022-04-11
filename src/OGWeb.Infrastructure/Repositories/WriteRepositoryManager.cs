using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Interfaces.Repositories.Videos;
using OGWeb.Core.Interfaces.Repositories.Works;
using OGWeb.Infrastructure.Context;
using OGWeb.Infrastructure.Repositories.Videos;
using OGWeb.Infrastructure.Repositories.Works;

namespace OGWeb.Infrastructure.Repositories;

public class WriteRepositoryManager : IWriteRepositoryManager
{

    private readonly Lazy<IWriteWorkRepository> _writeWorkRepository;
    private readonly Lazy<IWriteVideoRepository> _writeVideoRepository;

    public WriteRepositoryManager(ApplicationDbContext context)
    {
        _writeWorkRepository = new Lazy<IWriteWorkRepository>(() => new WriteWorkRepository(context));
        _writeVideoRepository = new Lazy<IWriteVideoRepository>(() => new WriteVideoRepository(context));
    }

    public IWriteWorkRepository WorkRepository => _writeWorkRepository.Value;

    public IWriteVideoRepository VideoRepository => _writeVideoRepository.Value;
}