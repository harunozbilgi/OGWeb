using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;
using MediaBalansDocument.Library;
using MediaBalansDocument.Library.Helpers;

namespace OGWeb.Core.Commands.Sliders;

public class CreateSliderCommand : IRequest<CustomResponse<NoContent>>
{
    public string Title { get; set; }
    public IFormFile File { get; set; }

    public class CreateSliderCommandHandler : IRequestHandler<CreateSliderCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSliderCommandHandler> _logger;

        public CreateSliderCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<CreateSliderCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
        {

            string image_Url = "";

            string path = "files/sliders/";

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
            var slider = _mapper.Map<Entities.Slider>(request);

            slider.ImageUrl = image_Url;

            await _writeRepositoryManager.SliderRepository.CreateSlidersync(slider);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}