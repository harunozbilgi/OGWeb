using AutoMapper;
using MediaBalansDocument.Library;
using MediaBalansDocument.Library.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.OverViews;

public class UpdateOverViewCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl_One { get; set; }
    public string ImageUrl_Two { get; set; }
    public IFormFile File_One { get; set; }
    public IFormFile File_Two { get; set; }

    public class UpdateOverViewCommandHandler : IRequestHandler<UpdateOverViewCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;

        private readonly IMapper _mapper;

        private readonly ILogger<UpdateOverViewCommandHandler> _logger;

        public UpdateOverViewCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<UpdateOverViewCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(UpdateOverViewCommand request, CancellationToken cancellationToken)
        {
            string path = "files/about/";

            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single overViewId: {request.Id}");
                throw new ArgumentNullException($"Querying for single overViewId: {request.Id}");
            }

            if (request == null)
            {
                _logger.LogError("eklemek istediginiz alanlar bos");

                throw new ArgumentNullException(nameof(request));
            }

            if (request.File_One != null)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl_One))
                {
                    FileUploader.FolderRemove(request.ImageUrl_One);
                }

                var response = await FileUploader.UploadAsync(request.File_One, path);

                request.ImageUrl_One = string.Concat(path, response.DocumentName);
                await ImageHelper.OptimizeAsync(request.ImageUrl_One);
            }
            
            if (request.File_Two != null)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl_Two))
                {
                    FileUploader.FolderRemove(request.ImageUrl_Two);
                }
                var response = await FileUploader.UploadAsync(request.File_Two, path);

                request.ImageUrl_Two = string.Concat(path, response.DocumentName);
                await ImageHelper.OptimizeAsync(request.ImageUrl_Two);
            }

            var overView = _mapper.Map<OverView>(request);

            await _writeRepositoryManager.OverViewRepository.UpdateOverViewAsync(overView);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}
