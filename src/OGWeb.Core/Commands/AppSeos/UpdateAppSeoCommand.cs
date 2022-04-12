using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.AppSeos;

public class UpdateAppSeoCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }
    public string Page { get; set; }
    public string Title { get; set; }
    public string Keyword { get; set; }
    public string Description { get; set; }

    public class UpdateAppSeoCommandHandler : IRequestHandler<UpdateAppSeoCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAppSeoCommandHandler> _logger;

        public UpdateAppSeoCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<UpdateAppSeoCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(UpdateAppSeoCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single appSeoId: {request.Id}");
                throw new ArgumentNullException($"Querying for single appSeoId: {request.Id}");
            }
            var appSeo = _mapper.Map<AppSeo>(request);

            await _writeRepositoryManager.AppSeoRepository.UpdateAppSeoAsync(appSeo);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}
