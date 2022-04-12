using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.AppSeos;

public  class GetAppSeoByIdQuery:IRequest<CustomResponse<AppSeoDto>>
{
    public Guid Id { get; set; }

    public class GetAppSeoByIdQueryHandler : IRequestHandler<GetAppSeoByIdQuery, CustomResponse<AppSeoDto>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAppSeoByIdQueryHandler> _logger;

        public GetAppSeoByIdQueryHandler(IReadRepositoryManager readRepositoryManager, IMapper mapper, ILogger<GetAppSeoByIdQueryHandler> logger)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<AppSeoDto>> Handle(GetAppSeoByIdQuery request, CancellationToken cancellationToken)
        {

            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single appSeoId: {request.Id}");

                throw new ArgumentNullException($"Querying for single appSeoId: {request.Id}");
            }
            var result = await _readRepositoryManager.AppSeoRepository.GetByIdAppSeoAsync(request.Id);

            if (result != null)
            {
                var response = _mapper.Map<AppSeoDto>(result);
                return CustomResponse<AppSeoDto>.Success(response, 200);
            }

            _logger.LogError($"No record found according to the {request.Id} Id you sent.");

            throw new Exception($"No record found according to the {request.Id} Id you sent.");
        }
    }
}
