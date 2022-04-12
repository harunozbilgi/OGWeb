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

public class ReadRepositoryManager : IReadRepositoryManager
{
    private readonly Lazy<IReadWorkRepository> _readWorkRepository;
    private readonly Lazy<IReadVideoRepository> _readVideoRepository;
    private readonly Lazy<IReadOverViewRepository> _readOverViewRepository;
    private readonly Lazy<IReadAppSeoRepository> _readAppSeoRepository;

    public ReadRepositoryManager(ApplicationDbContext context)
    {
        _readWorkRepository = new Lazy<IReadWorkRepository>(() => new ReadWorkRepository(context));
        _readVideoRepository = new Lazy<IReadVideoRepository>(() => new ReadVideoRepository(context));
        _readOverViewRepository = new Lazy<IReadOverViewRepository>(() => new ReadOverViewRepository(context));
        _readAppSeoRepository = new Lazy<IReadAppSeoRepository>(() => new ReadAppSeoRepository(context));
    }

    public IReadWorkRepository WorkRepository => _readWorkRepository.Value;
    public IReadVideoRepository VideoRepository => _readVideoRepository.Value;
    public IReadOverViewRepository OverViewRepository => _readOverViewRepository.Value;
    public IReadAppSeoRepository AppSeoRepository => _readAppSeoRepository.Value;
}