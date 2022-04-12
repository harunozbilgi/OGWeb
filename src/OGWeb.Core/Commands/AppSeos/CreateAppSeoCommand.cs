using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.AppSeos;

public class CreateAppSeoCommand : IRequest<CustomResponse<NoContent>>
{
    public string Page { get; set; }
    public string Title { get; set; }
    public string Keyword { get; set; }
    public string Description { get; set; }

    public class CreateAppSeoCommandHandler : IRequestHandler<CreateAppSeoCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAppSeoCommandHandler> _logger;

        public CreateAppSeoCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<CreateAppSeoCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(CreateAppSeoCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogError("eklemek istediginiz alanlar bos");

                throw new ArgumentNullException(nameof(request));
            }

            var appSeo = _mapper.Map<AppSeo>(request);

            await _writeRepositoryManager.AppSeoRepository.CreateAppSeoAsync(appSeo);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}
