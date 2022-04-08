using AutoMapper;
using MediatR;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.Works;

public class GetAllWorkQuery : IRequest<CustomResponse<List<WorkDto>>>
{

    public class GetAllWorkQueryHandler : IRequestHandler<GetAllWorkQuery, CustomResponse<List<WorkDto>>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;

        public GetAllWorkQueryHandler(IReadRepositoryManager readRepositoryManager, IMapper mapper)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<WorkDto>>> Handle(GetAllWorkQuery request, CancellationToken cancellationToken)
        {
            var result = await _readRepositoryManager.WorkRepository.GetWorkListAsync();

            var respone = _mapper.Map<List<WorkDto>>(result);

            return CustomResponse<List<WorkDto>>.Success(respone, 200);
        }
    }
}