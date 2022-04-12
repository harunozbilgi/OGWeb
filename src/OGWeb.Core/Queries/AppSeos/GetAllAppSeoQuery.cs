using AutoMapper;
using MediatR;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.AppSeos;

public class GetAllAppSeoQuery : IRequest<CustomResponse<List<AppSeoDto>>>
{
    public class GetAllAppSeoQueryHandler : IRequestHandler<GetAllAppSeoQuery, CustomResponse<List<AppSeoDto>>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;

        public GetAllAppSeoQueryHandler(IReadRepositoryManager readRepositoryManager, IMapper mapper)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<AppSeoDto>>> Handle(GetAllAppSeoQuery request, CancellationToken cancellationToken)
        {
            var result = await _readRepositoryManager.AppSeoRepository.GetAppSeoListAsync();

            var respone = _mapper.Map<List<AppSeoDto>>(result);

            return CustomResponse<List<AppSeoDto>>.Success(respone, 200);
        }
    }
}
