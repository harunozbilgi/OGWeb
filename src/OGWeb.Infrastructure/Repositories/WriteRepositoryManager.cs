using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Interfaces.Repositories.AppSeos;
using OGWeb.Core.Interfaces.Repositories.AppSettings;
using OGWeb.Core.Interfaces.Repositories.OverViews;
using OGWeb.Core.Interfaces.Repositories.Sliders;
using OGWeb.Core.Interfaces.Repositories.Videos;
using OGWeb.Core.Interfaces.Repositories.Works;
using OGWeb.Infrastructure.Context;
using OGWeb.Infrastructure.Repositories.AppSeos;
using OGWeb.Infrastructure.Repositories.AppSettings;
using OGWeb.Infrastructure.Repositories.OverViews;
using OGWeb.Infrastructure.Repositories.Sliders;
using OGWeb.Infrastructure.Repositories.Videos;
using OGWeb.Infrastructure.Repositories.Works;

namespace OGWeb.Infrastructure.Repositories;

public class WriteRepositoryManager : IWriteRepositoryManager
{

    private readonly Lazy<IWriteWorkRepository> _writeWorkRepository;
    private readonly Lazy<IWriteVideoRepository> _writeVideoRepository;
    private readonly Lazy<IWriteOverViewRepository> _writeOverViewRepository;
    private readonly Lazy<IWriteAppSeoRepository> _writeAppSeoRepository;
    private readonly Lazy<IWriteSliderRepository> _writeSliderRepository;
    private readonly Lazy<IWriteAppSettingRepository> _writeAppSettingRepository;

    public WriteRepositoryManager(ApplicationDbContext context)
    {
        _writeWorkRepository = new Lazy<IWriteWorkRepository>(() => new WriteWorkRepository(context));
        _writeVideoRepository = new Lazy<IWriteVideoRepository>(() => new WriteVideoRepository(context));
        _writeOverViewRepository = new Lazy<IWriteOverViewRepository>(() => new WriteOverViewRepository(context));
        _writeAppSeoRepository = new Lazy<IWriteAppSeoRepository>(() => new WriteAppSeoRepository(context));
        _writeSliderRepository = new Lazy<IWriteSliderRepository>(() => new WriteSliderRepository(context));
        _writeAppSettingRepository = new Lazy<IWriteAppSettingRepository>(() => new WriteAppSettingRepository(context));
    }

    public IWriteWorkRepository WorkRepository => _writeWorkRepository.Value;
    public IWriteVideoRepository VideoRepository => _writeVideoRepository.Value;
    public IWriteOverViewRepository OverViewRepository => _writeOverViewRepository.Value;
    public IWriteAppSeoRepository AppSeoRepository => _writeAppSeoRepository.Value;
    public IWriteSliderRepository SliderRepository => _writeSliderRepository.Value;
    public IWriteAppSettingRepository AppSettingRepository => _writeAppSettingRepository.Value;
}