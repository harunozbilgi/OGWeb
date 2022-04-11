using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Works;

public class UpdateWorkCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
     public string Keyword_Seo { get; set; }
    public string Description_Seo { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool? IsActived { get; set; }

    public class UpdateWorkCommandHandler : IRequestHandler<UpdateWorkCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateWorkCommandHandler> _logger;

        public UpdateWorkCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<UpdateWorkCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(UpdateWorkCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single categoryId: {request.Id}");
                throw new ArgumentNullException($"Querying for single categoryId: {request.Id}");
            }
            var work=_mapper.Map<Work>(request);
            
            var result = await _writeRepositoryManager.WorkRepository.UpdateWorkAsync(work);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}