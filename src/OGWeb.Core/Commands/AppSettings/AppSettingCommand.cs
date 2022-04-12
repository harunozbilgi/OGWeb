using AutoMapper;
using MediaBalansDocument.Library;
using MediaBalansDocument.Library.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Dtos;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.AppSettings;

public class AppSettingCommand : IRequest<CustomResponse<AppSettingDto>>
{
    public string FaceBook { get; set; }
    public string Instagram { get; set; }
    public string LinkedIn { get; set; }
    public string YouTube { get; set; }
    public string Twitter { get; set; }
    public string GooglePixel { get; set; }
    public IFormFile File { get; set; }

    public class AppSettingCommandHandler : IRequestHandler<AppSettingCommand, CustomResponse<AppSettingDto>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AppSettingCommandHandler> _logger;

        public AppSettingCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<AppSettingCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<AppSettingDto>> Handle(AppSettingCommand request, CancellationToken cancellationToken)
        {
            string image_Url = "";

            string path = "files/logo/";

            if (request == null)
            {
                _logger.LogError("eklemek istediginiz alanlar bos");

                throw new ArgumentNullException(nameof(request));
            }
            if (request.File != null)
            {
                var responseFile = await FileUploader.UploadAsync(request.File, path);

                image_Url += string.Concat(path, responseFile.DocumentName);
                await ImageHelper.OptimizeAsync(image_Url);
            }
            var appSetting = _mapper.Map<AppSetting>(request);

            appSetting.LogoUrl = image_Url;

            var result = await _writeRepositoryManager.AppSettingRepository.AppSettingAsync(appSetting);
            
            var response = _mapper.Map<AppSettingDto>(result);

            return CustomResponse<AppSettingDto>.Success(response, 204);
        }
    }
}