using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Settings;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.Works;

public class GetWorkByIdQuery : IRequest<CustomResponse<WorkDto>>
{
    public Guid Id { get; set; }

    public class GetWorkByIdQueryHandler : IRequestHandler<GetWorkByIdQuery, CustomResponse<WorkDto>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<GetWorkByIdQueryHandler> _logger;
        private readonly DocumentSetting _documentSetting;

        public GetWorkByIdQueryHandler(IReadRepositoryManager readRepositoryManager, IMapper mapper, ILogger<GetWorkByIdQueryHandler> logger, IOptions<DocumentSetting> documentSetting)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _logger = logger;
            _documentSetting = documentSetting.Value;
        }

        public async Task<CustomResponse<WorkDto>> Handle(GetWorkByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single categoryId: {request.Id}");

                throw new ArgumentNullException($"Querying for single categoryId: {request.Id}");
            }
            var result = await _readRepositoryManager.WorkRepository.GetByIdWorkAsync(request.Id);

            if (result != null)
            {
                var response = _mapper.Map<WorkDto>(result);

                return CustomResponse<WorkDto>.Success(response, 200);
            }

            _logger.LogError($"No record found according to the {request.Id} Id you sent.");

            throw new Exception($"No record found according to the {request.Id} Id you sent.");
        }
    }
}