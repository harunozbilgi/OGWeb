using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Settings;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.Videos;

public class GetVideoByIdQuery : IRequest<CustomResponse<VideoDto>>
{
    public Guid Id { get; set; }

    public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, CustomResponse<VideoDto>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;

        private readonly ILogger<GetVideoByIdQueryHandler> _logger;
        private readonly DocumentSetting _documentSetting;

        public GetVideoByIdQueryHandler(IReadRepositoryManager readRepositoryManager,
                                        IMapper mapper,
                                        ILogger<GetVideoByIdQueryHandler> logger,
                                        IOptions<DocumentSetting> documentSetting)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _logger = logger;
            _documentSetting = documentSetting.Value;
        }

        public async Task<CustomResponse<VideoDto>> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single videoId: {request.Id}");

                throw new ArgumentNullException($"Querying for single videoId: {request.Id}");
            }
            var result = await _readRepositoryManager.VideoRepository.GetByIdVideoAsync(request.Id);

            if (result != null)
            {
                var response = _mapper.Map<VideoDto>(result);
                return CustomResponse<VideoDto>.Success(response, 200);
            }

            _logger.LogError($"No record found according to the {request.Id} Id you sent.");

            throw new Exception($"No record found according to the {request.Id} Id you sent.");
        }
    }
}