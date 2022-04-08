using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Interfaces.Repositories.Works;
using OGWeb.Infrastructure.Context;
using OGWeb.Infrastructure.Repositories.Works;

namespace OGWeb.Infrastructure.Repositories;

public class ReadRepositoryManager : IReadRepositoryManager
{
    private readonly Lazy<IReadWorkRepository> _readWorkRepository;

    public ReadRepositoryManager(ApplicationDbContext context)
    {
        _readWorkRepository = new Lazy<IReadWorkRepository>(() => new ReadWorkRepository(context));
    }
    
    public IReadWorkRepository WorkRepository => _readWorkRepository.Value;
}