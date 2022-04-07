using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Interfaces.Services;

namespace OGWeb.Infrastructure.Services;

public class WorkService : IWorkService
{
    private readonly IWorkRepository _workRepository;

    public WorkService(IWorkRepository workRepository)
    {
        _workRepository = workRepository;
    }
    public async Task<Work> AddWorkAsync(Work work)
    {
        var result = await _workRepository.AddAsync(work);
        return result;
    }
    public async Task<Work> GetByIdWorkAsync(Guid id)
    {
        var result = await _workRepository.GetByAsync(id);
        return result;
    }
    public async Task<IReadOnlyCollection<Work>> GetWorkListAsync()
    {
        var result = await _workRepository.GetAllAsync(o => o.OrderByDescending(x => x.CreatedDate));
        return result.ToList();
    }
    public async Task<bool> RemoveWorkAsync(Work work)
    {
        await _workRepository.DeleteAsync(work);
        return true;
    }
    public async Task<bool> UpdateWorkAsync(Work work)
    {
        await _workRepository.DeleteAsync(work);
        return true;
    }
}