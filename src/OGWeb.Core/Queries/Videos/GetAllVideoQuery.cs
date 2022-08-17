using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Settings;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.Videos;

public class GetAllVideoQuery : IRequest<CustomResponse<List<VideoDto>>>
{
    public class GetAllVideoQueryHandler : IRequestHandler<GetAllVideoQuery, CustomResponse<List<VideoDto>>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;
        private readonly DocumentSetting _documentSetting;

        public GetAllVideoQueryHandler(IReadRepositoryManager readRepositoryManager, IMapper mapper, IOptions<DocumentSetting> documentSetting)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _documentSetting = documentSetting.Value;
        }

        public async Task<CustomResponse<List<VideoDto>>> Handle(GetAllVideoQuery request, CancellationToken cancellationToken)
        {
            var result = await _readRepositoryManager.VideoRepository.GetVideoListAsync();

            var respone = _mapper.Map<List<VideoDto>>(result);
            
            return CustomResponse<List<VideoDto>>.Success(respone, 200);
        }
    }
}