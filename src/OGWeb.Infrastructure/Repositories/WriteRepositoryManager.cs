using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Interfaces.Repositories.AppSeos;
using OGWeb.Core.Interfaces.Repositories.OverViews;
using OGWeb.Core.Interfaces.Repositories.Videos;
using OGWeb.Core.Interfaces.Repositories.Works;
using OGWeb.Infrastructure.Context;
using OGWeb.Infrastructure.Repositories.AppSeos;
using OGWeb.Infrastructure.Repositories.OverViews;
using OGWeb.Infrastructure.Repositories.Videos;
using OGWeb.Infrastructure.Repositories.Works;

namespace OGWeb.Infrastructure.Repositories;

public class WriteRepositoryManager : IWriteRepositoryManager
{

    private readonly Lazy<IWriteWorkRepository> _writeWorkRepository;
    private readonly Lazy<IWriteVideoRepository> _writeVideoRepository;
    private readonly Lazy<IWriteOverViewRepository> _writeOverViewRepository;
    private readonly Lazy<IWriteAppSeoRepository> _writeAppSeoRepository;

    public WriteRepositoryManager(ApplicationDbContext context)
    {
        _writeWorkRepository = new Lazy<IWriteWorkRepository>(() => new WriteWorkRepository(context));
        _writeVideoRepository = new Lazy<IWriteVideoRepository>(() => new WriteVideoRepository(context));
        _writeOverViewRepository = new Lazy<IWriteOverViewRepository>(() => new WriteOverViewRepository(context));
        _writeAppSeoRepository = new Lazy<IWriteAppSeoRepository>(() => new WriteAppSeoRepository(context));
    }

    public IWriteWorkRepository WorkRepository => _writeWorkRepository.Value;
    public IWriteVideoRepository VideoRepository => _writeVideoRepository.Value;
    public IWriteOverViewRepository OverViewRepository => _writeOverViewRepository.Value;
    public IWriteAppSeoRepository AppSeoRepository => _writeAppSeoRepository.Value;
}