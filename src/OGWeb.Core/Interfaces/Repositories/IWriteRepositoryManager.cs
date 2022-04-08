using OGWeb.Core.Interfaces.Repositories.Works;

namespace OGWeb.Core.Interfaces.Repositories;

public interface IWriteRepositoryManager
{
    public IWriteWorkRepository WorkRepository { get; }
}