using AutoMapper;
using MediaBalansDocument.Library;
using MediaBalansDocument.Library.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Videos;

public class UpdateVideoCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public string Url { get; set; }
    public IFormFile File { get; set; }
    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateVideoCommandHandler> _logger;

        public UpdateVideoCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<UpdateVideoCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single videoId: {request.Id}");
                throw new ArgumentNullException($"Querying for single videoId: {request.Id}");
            }
            string path = "files/videos/";

            if (request.File != null)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl))
                {
                    FileUploader.FolderRemove(request.ImageUrl);
                }
                var response = await FileUploader.UploadAsync(request.File, path);

                request.ImageUrl = string.Concat(path, response.DocumentName);

                await ImageHelper.OptimizeAsync(request.ImageUrl);
            }
            var video = _mapper.Map<Video>(request);

            var result = await _writeRepositoryManager.VideoRepository.UpdateVideoAsync(video);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}