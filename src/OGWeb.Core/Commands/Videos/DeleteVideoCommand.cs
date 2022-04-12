using MediaBalansDocument.Library;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Videos;

public class DeleteVideoCommand:IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly ILogger<DeleteVideoCommandHandler> _logger;

        public DeleteVideoCommandHandler(IWriteRepositoryManager writeRepositoryManager, IReadRepositoryManager readRepositoryManager, ILogger<DeleteVideoCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _readRepositoryManager = readRepositoryManager;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single videoId: {request.Id}");
                throw new ArgumentNullException($"Querying for single videoId: {request.Id}");
            }
            var result = await _readRepositoryManager.VideoRepository.GetByIdVideoAsync(request.Id);

            if(result == null)
            {
                _logger.LogError($"Querying for single videoId: {result.Id}");
                throw new ArgumentNullException($"Querying for single videoId: {result.Id}");
            }

            if (!string.IsNullOrEmpty(result.ImageUrl))
            {
                FileUploader.FolderRemove(result.ImageUrl);
            }

            await _writeRepositoryManager.VideoRepository.RemoveVideoAsync(request.Id);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}