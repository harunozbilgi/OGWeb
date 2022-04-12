using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Settings;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.AppSettings;

public class GetAppSettingByQuery : IRequest<CustomResponse<AppSettingDto>>
{
    public class GetAppSettingByQueryHandler : IRequestHandler<GetAppSettingByQuery, CustomResponse<AppSettingDto>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;
        private readonly DocumentSetting _documentSetting;

        public GetAppSettingByQueryHandler(IReadRepositoryManager readRepositoryManager,
                                           IMapper mapper,
                                           IOptions<DocumentSetting> documentSetting)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _documentSetting = documentSetting.Value;
        }

        public async Task<CustomResponse<AppSettingDto>> Handle(GetAppSettingByQuery request, CancellationToken cancellationToken)
        {
            var result = await _readRepositoryManager.AppSettingRepository.GetAppSettingByAsync();

            var response = _mapper.Map<AppSettingDto>(result);

            response.LogoUrl = string.Concat(_documentSetting.StorageUrl, response.LogoUrl);

            return CustomResponse<AppSettingDto>.Success(response, 200);
        }
    }
}