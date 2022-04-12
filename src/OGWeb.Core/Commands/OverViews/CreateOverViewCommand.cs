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

public class CreateOverViewCommand : IRequest<CustomResponse<NoContent>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile File_One { get; set; }
    public IFormFile File_Two { get; set; }

    public class CreateOverViewCommandHandler : IRequestHandler<CreateOverViewCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOverViewCommandHandler> _logger;

        public CreateOverViewCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<CreateOverViewCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(CreateOverViewCommand request, CancellationToken cancellationToken)
        {
            string image_Url_One = "";

            string image_Url_Two = "";

            string path = "files/about/";

            if (request == null)
            {
                _logger.LogError("eklemek istediginiz alanlar bos");

                throw new ArgumentNullException(nameof(request));
            }

            if (request.File_One != null)
            {
                var response = await FileUploader.UploadAsync(request.File_One, path);

                image_Url_One += string.Concat(path, response.DocumentName);
                await ImageHelper.OptimizeAsync(image_Url_One);
            }
            if (request.File_Two != null)
            {
                var response = await FileUploader.UploadAsync(request.File_Two, path);

                image_Url_Two += string.Concat(path, response.DocumentName);
                await ImageHelper.OptimizeAsync(image_Url_Two);
            }

            var overView = _mapper.Map<OverView>(request);

            overView.ImageUrl_One = image_Url_One;
            overView.ImageUrl_Two = image_Url_Two;

            await _writeRepositoryManager.OverViewRepository.CreateOverViewAsync(overView);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}
