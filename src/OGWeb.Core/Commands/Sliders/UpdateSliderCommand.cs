using AutoMapper;
using MediaBalansDocument.Library;
using MediaBalansDocument.Library.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Sliders;

public class UpdateSliderCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public IFormFile File { get; set; }
    public class UpdateSliderCommandHandler : IRequestHandler<UpdateSliderCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSliderCommandHandler> _logger;

        public UpdateSliderCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<UpdateSliderCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(UpdateSliderCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single sliderId: {request.Id}");
                throw new ArgumentNullException($"Querying for single sliderId: {request.Id}");
            }
            string path = "files/sliders/";

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
            var update = _mapper.Map<Entities.Slider>(request);

            var result = await _writeRepositoryManager.SliderRepository.UpdateSliderAsync(update);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}