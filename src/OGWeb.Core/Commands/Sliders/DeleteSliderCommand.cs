using MediaBalansDocument.Library;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Sliders;

public class DeleteSliderCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }
    public class DeleteSliderCommandHandler : IRequestHandler<DeleteSliderCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly ILogger<DeleteSliderCommandHandler> _logger;

        public DeleteSliderCommandHandler(IWriteRepositoryManager writeRepositoryManager, IReadRepositoryManager readRepositoryManager, ILogger<DeleteSliderCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _readRepositoryManager = readRepositoryManager;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single sliderId: {request.Id}");
                throw new ArgumentNullException($"Querying for single sliderId: {request.Id}");
            }
            var result = await _readRepositoryManager.SliderRepository.GetByIdSliderAsync(request.Id);

            if (result == null)
            {
                _logger.LogError($"Querying for single sliderId: {result.Id}");
                throw new ArgumentNullException($"Querying for single sliderId: {result.Id}");
            }

            if (!string.IsNullOrEmpty(result.ImageUrl))
            {
                FileUploader.FolderRemove(result.ImageUrl);
            }

            await _writeRepositoryManager.SliderRepository.RemoveSliderAsync(request.Id);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}