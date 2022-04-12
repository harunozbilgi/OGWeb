using OGWeb.Core.Interfaces.Repositories.AppSeos;
using OGWeb.Core.Interfaces.Repositories.OverViews;
using OGWeb.Core.Interfaces.Repositories.Videos;
using OGWeb.Core.Interfaces.Repositories.Works;

namespace OGWeb.Core.Interfaces.Repositories;

public interface IWriteRepositoryManager
{
    public IWriteWorkRepository WorkRepository { get; }
    public IWriteVideoRepository VideoRepository { get;}
    public IWriteOverViewRepository OverViewRepository { get; } 
    public IWriteAppSeoRepository AppSeoRepository { get; }
}