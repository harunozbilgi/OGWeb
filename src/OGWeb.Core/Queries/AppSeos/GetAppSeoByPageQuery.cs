using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.AppSeos;

public class GetAppSeoByPageQuery : IRequest<CustomResponse<AppSeoDto>>
{
    public string Page { get; set; }

    public class GetAppSeoByPageQueryHandler : IRequestHandler<GetAppSeoByPageQuery, CustomResponse<AppSeoDto>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAppSeoByPageQueryHandler> _logger;

        public GetAppSeoByPageQueryHandler(IReadRepositoryManager readRepositoryManager, IMapper mapper, ILogger<GetAppSeoByPageQueryHandler> logger)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<AppSeoDto>> Handle(GetAppSeoByPageQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Page))
            {
                _logger.LogError($"Querying for single page: {request.Page}");

                throw new ArgumentNullException($"Querying for single page: {request.Page}");
            }
            var result = await _readRepositoryManager.AppSeoRepository.GetByPageAppSeoAsync(request.Page);

            if (result != null)
            {
                var response = _mapper.Map<AppSeoDto>(result);
                return CustomResponse<AppSeoDto>.Success(response, 200);
            }

            _logger.LogError($"No record found according to the {request.Page} page you sent.");

            throw new Exception($"No record found according to the {request.Page} page you sent.");
        }
    }
}
