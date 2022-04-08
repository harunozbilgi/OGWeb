using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Works;

public class DeleteWorkCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }

    public class DeleteWorkCommandHandler : IRequestHandler<DeleteWorkCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly ILogger<DeleteWorkCommandHandler> _logger;

        public DeleteWorkCommandHandler(IWriteRepositoryManager writeRepositoryManager, ILogger<DeleteWorkCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(DeleteWorkCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single categoryId: {request.Id}");
                throw new ArgumentNullException($"Querying for single categoryId: {request.Id}");
            }
            await _writeRepositoryManager.WorkRepository.RemoveWorkAsync(request.Id);
            return CustomResponse<NoContent>.Success(204);
        }
    }
}