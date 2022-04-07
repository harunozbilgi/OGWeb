using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Services;

public interface IWorkService
{
    Task<IReadOnlyCollection<Work>> GetWorkListAsync();
    Task<Work> GetByIdWorkAsync(Guid id);
    Task<Work> AddWorkAsync(Work work);
    Task<bool> UpdateWorkAsync(Work work);
    Task<bool> RemoveWorkAsync(Work work);
}
