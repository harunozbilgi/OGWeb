using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Settings;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.Sliders;

public class GetSliderByIdQuery : IRequest<CustomResponse<SliderDto>>
{
    public Guid Id { get; set; }

    public class GetSliderByIdQueryHandler : IRequestHandler<GetSliderByIdQuery, CustomResponse<SliderDto>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;

        private readonly ILogger<GetSliderByIdQueryHandler> _logger;
        private readonly DocumentSetting _documentSetting;

        public GetSliderByIdQueryHandler(IReadRepositoryManager readRepositoryManager,
                                        IMapper mapper,
                                        ILogger<GetSliderByIdQueryHandler> logger,
                                        IOptions<DocumentSetting> documentSetting)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _logger = logger;
            _documentSetting = documentSetting.Value;
        }

        public async Task<CustomResponse<SliderDto>> Handle(GetSliderByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single sliderId: {request.Id}");

                throw new ArgumentNullException($"Querying for single sliderId: {request.Id}");
            }
            var result = await _readRepositoryManager.SliderRepository.GetByIdSliderAsync(request.Id);

            if (result != null)
            {
                var response = _mapper.Map<SliderDto>(result);
                response.ImageUrl = string.Concat(_documentSetting.StorageUrl, response.ImageUrl);
                return CustomResponse<SliderDto>.Success(response, 200);
            }

            _logger.LogError($"No record found according to the {request.Id} Id you sent.");

            throw new Exception($"No record found according to the {request.Id} Id you sent.");
        }
    }
}