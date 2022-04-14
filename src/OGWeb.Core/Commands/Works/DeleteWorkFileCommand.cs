using MediaBalansDocument.Library;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Works;

public class DeleteWorkFileCommand : IRequest<CustomResponse<bool>>
{
    public Guid WorkId { get; set; }

    public class DeleteWorkFileCommandHandler : IRequestHandler<DeleteWorkFileCommand, CustomResponse<bool>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;

        private readonly IWriteRepositoryManager _writeRepositoryManager;

        private readonly ILogger<DeleteWorkFileCommandHandler> _logger;

        public DeleteWorkFileCommandHandler(IReadRepositoryManager readRepositoryManager,
                                                IWriteRepositoryManager writeRepositoryManager,
                                                ILogger<DeleteWorkFileCommandHandler> logger)
        {
            _readRepositoryManager = readRepositoryManager;
            _writeRepositoryManager = writeRepositoryManager;
            _logger = logger;
        }

        public async Task<CustomResponse<bool>> Handle(DeleteWorkFileCommand request, CancellationToken cancellationToken)
        {
            if (request.WorkId == Guid.Empty)
            {
                _logger.LogError($"Querying for single categoryId: {request.WorkId}");

                throw new ArgumentNullException($"Querying for single categoryId: {request.WorkId}");
            }
            var result = await _readRepositoryManager.WorkRepository.GetAllWorkFileAsync(request.WorkId);

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
                return CustomResponse<bool>.Success(200);
            }

            _logger.LogError($"No record found according to the {request.WorkId} Id you sent.");

            throw new Exception($"No record found according to the {request.WorkId} Id you sent.");
        }
    }
}
