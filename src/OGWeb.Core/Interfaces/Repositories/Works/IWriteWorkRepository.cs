using OGWeb.Core.Entities;

namespace OGWeb.Core.Interfaces.Repositories.Works;

public interface IWriteWorkRepository
{
    Task<Work> CreateWorkAsync(Work work);
    Task<bool> UpdateWorkAsync(Work work);
    Task<bool> RemoveWorkAsync(Guid workId);
}