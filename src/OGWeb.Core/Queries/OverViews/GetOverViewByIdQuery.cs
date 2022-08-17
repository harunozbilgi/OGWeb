using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Settings;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.OverViews;

public class GetOverViewByIdQuery : IRequest<CustomResponse<OverViewDto>>
{
    public Guid Id { get; set; }

    public class GetOverViewByIdQueryHandler : IRequestHandler<GetOverViewByIdQuery, CustomResponse<OverViewDto>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;

        private readonly ILogger<GetOverViewByIdQueryHandler> _logger;
        private readonly DocumentSetting _documentSetting;

        public GetOverViewByIdQueryHandler(IReadRepositoryManager readRepositoryManager,
                                        IMapper mapper,
                                        ILogger<GetOverViewByIdQueryHandler> logger,
                                        IOptions<DocumentSetting> documentSetting)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _logger = logger;
            _documentSetting = documentSetting.Value;
        }

        public async Task<CustomResponse<OverViewDto>> Handle(GetOverViewByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single videoId: {request.Id}");

                throw new ArgumentNullException($"Querying for single videoId: {request.Id}");
            }
            var result = await _readRepositoryManager.OverViewRepository.GetByIdOverViewAsync(request.Id);

            if (result != null)
            {
                var response = _mapper.Map<OverViewDto>(result);
                return CustomResponse<OverViewDto>.Success(response, 200);
            }

            _logger.LogError($"No record found according to the {request.Id} Id you sent.");

            throw new Exception($"No record found according to the {request.Id} Id you sent.");
        }
    }
}