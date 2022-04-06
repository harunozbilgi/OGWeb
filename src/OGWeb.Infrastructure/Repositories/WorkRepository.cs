using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure.Repositories;

public class WorkRepository : GenericRepositoryAsync<Work>, IWorkRepository
{
    public WorkRepository(ApplicationDbContext context) : base(context)
    {

    }
}
