using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;
using MediaBalansDocument.Library;
using OGWeb.Core.Entities;
using MediaBalansDocument.Library.Helpers;

namespace OGWeb.Core.Commands.Videos;

public class CreateVideoCommand : IRequest<CustomResponse<NoContent>>
{
    public string Title { get; set; }
    public string Url { get; set; }
    public IFormFile File { get; set; }

    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateVideoCommandHandler> _logger;

        public CreateVideoCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<CreateVideoCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {

            string image_Url = "";

            string path = "files/videos/";

            if (request == null)
            {
                _logger.LogError("eklemek istediginiz alanlar bos");

                throw new ArgumentNullException(nameof(request));
            }

            if (request.File != null)
            {
                var response = await FileUploader.UploadAsync(request.File, path);

                image_Url += string.Concat(path, response.DocumentName);
                await ImageHelper.OptimizeAsync(image_Url);
            }
            var video = _mapper.Map<Video>(request);

            video.ImageUrl = image_Url;

            await _writeRepositoryManager.VideoRepository.CreateVideoAsync(video);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}