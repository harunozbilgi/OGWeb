using OGWeb.Core.Interfaces.Repositories.AppSeos;
using OGWeb.Core.Interfaces.Repositories.AppSettings;
using OGWeb.Core.Interfaces.Repositories.OverViews;
using OGWeb.Core.Interfaces.Repositories.Sliders;
using OGWeb.Core.Interfaces.Repositories.Videos;
using OGWeb.Core.Interfaces.Repositories.Works;

namespace OGWeb.Core.Interfaces.Repositories;

public interface IReadRepositoryManager
{
    public IReadWorkRepository WorkRepository { get; }
    public IReadVideoRepository VideoRepository { get; }
    public IReadOverViewRepository OverViewRepository { get; }
    public IReadAppSeoRepository AppSeoRepository { get; }
    public IReadSliderRepository SliderRepository { get; }
    public IReadAppSettingRepository AppSettingRepository {get;}
}