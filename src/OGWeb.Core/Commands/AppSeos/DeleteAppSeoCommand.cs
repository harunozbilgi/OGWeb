using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.AppSeos;

public class DeleteAppSeoCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }

    public class DeleteAppSeoCommandHandler : IRequestHandler<DeleteAppSeoCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly ILogger<DeleteAppSeoCommandHandler> _logger;

        public DeleteAppSeoCommandHandler(IWriteRepositoryManager writeRepositoryManager, ILogger<DeleteAppSeoCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(DeleteAppSeoCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single overViewId: {request.Id}");
                throw new ArgumentNullException($"Querying for single overViewId: {request.Id}");
            }

            await _writeRepositoryManager.AppSeoRepository.RemoveAppSeoAsync(request.Id);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}
