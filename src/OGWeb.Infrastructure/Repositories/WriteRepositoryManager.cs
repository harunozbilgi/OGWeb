using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Interfaces.Repositories.Works;
using OGWeb.Infrastructure.Context;
using OGWeb.Infrastructure.Repositories.Works;

namespace OGWeb.Infrastructure.Repositories;

public class WriteRepositoryManager : IWriteRepositoryManager
{

    private readonly Lazy<IWriteWorkRepository> _writeWorkRepository;

    public WriteRepositoryManager(ApplicationDbContext context)
    {
        _writeWorkRepository = new Lazy<IWriteWorkRepository>(() => new WriteWorkRepository(context));
    }

    public IWriteWorkRepository WorkRepository => _writeWorkRepository.Value;
}