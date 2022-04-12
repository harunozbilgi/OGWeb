using MediaBalansDocument.Library;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.OverViews;

public class DeleteOverViewCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }
    public class DeleteOverViewCommandHandler : IRequestHandler<DeleteOverViewCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly ILogger<DeleteOverViewCommandHandler> _logger;

        public DeleteOverViewCommandHandler(IWriteRepositoryManager writeRepositoryManager, IReadRepositoryManager readRepositoryManager, ILogger<DeleteOverViewCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _readRepositoryManager = readRepositoryManager;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(DeleteOverViewCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single overViewId: {request.Id}");
                throw new ArgumentNullException($"Querying for single overViewId: {request.Id}");
            }
            var result = await _readRepositoryManager.OverViewRepository.GetByIdOverViewAsync(request.Id);

            if(result == null)
            {
                _logger.LogError($"Querying for single overViewId: {result.Id}");
                throw new ArgumentNullException($"Querying for single overViewId: {result.Id}");
            }

            if (!string.IsNullOrEmpty(result.ImageUrl_One) )
            {
                FileUploader.FolderRemove(result.ImageUrl_One);
            }

            if (!string.IsNullOrEmpty(result.ImageUrl_Two))
            {
                FileUploader.FolderRemove(result.ImageUrl_Two);
            }

            await _writeRepositoryManager.OverViewRepository.RemoveOverViewAsync(request.Id);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}