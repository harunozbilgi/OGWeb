using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Dtos;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Works;

public class CreateWorkCommand : IRequest<CustomResponse<WorkDto>>
{
    public string AppSeoCode { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SlugUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool? IsActived { get; set; }

    public class CreateWorkCommandHandler : IRequestHandler<CreateWorkCommand, CustomResponse<WorkDto>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateWorkCommandHandler> _logger;

        public CreateWorkCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<CreateWorkCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<WorkDto>> Handle(CreateWorkCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogError("eklemek istediginiz alanlar bos");

                throw new Exception("no data");
            }
            
            var work = _mapper.Map<Work>(request);

            var result = await _writeRepositoryManager.WorkRepository.CreateWorkAsync(work);

            var response = _mapper.Map<WorkDto>(result);

            return CustomResponse<WorkDto>.Success(response, 201);

        }
    }
}