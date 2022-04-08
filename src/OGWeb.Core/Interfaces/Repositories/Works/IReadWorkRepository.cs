using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.Works;

public interface IReadWorkRepository
{
    Task<IReadOnlyCollection<Work>> GetWorkListAsync();
    Task<Work> GetByIdWorkAsync(Guid id);
}