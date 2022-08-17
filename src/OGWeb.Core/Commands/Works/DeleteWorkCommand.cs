using MediaBalansDocument.Library;
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
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly ILogger<DeleteWorkCommandHandler> _logger;

        public DeleteWorkCommandHandler(IWriteRepositoryManager writeRepositoryManager, IReadRepositoryManager readRepositoryManager, ILogger<DeleteWorkCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _readRepositoryManager = readRepositoryManager;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(DeleteWorkCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single categoryId: {request.Id}");
                throw new ArgumentNullException($"Querying for single categoryId: {request.Id}");
            }
            var result = await _readRepositoryManager.WorkRepository.GetAllWorkFileAsync(request.Id);

            if (result.Any())
            {
                foreach (var item in result)
                {
                    if (!string.IsNullOrEmpty(item.ImageUrl))
                    {
                        FileUploader.FolderRemove(item.ImageUrl);
                    }
                    await _writeRepositoryManager.WorkRepository.RemoveWorkFileAsync(item.Id);

                }
            }

            await _writeRepositoryManager.WorkRepository.RemoveWorkAsync(request.Id);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}