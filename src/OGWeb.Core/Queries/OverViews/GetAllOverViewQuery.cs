using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Settings;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.OverViews;

public class GetAllOverViewQuery : IRequest<CustomResponse<List<OverViewDto>>>
{
    public class GetAllOverViewQueryHandler : IRequestHandler<GetAllOverViewQuery, CustomResponse<List<OverViewDto>>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;
        private readonly DocumentSetting _documentSetting;

        public GetAllOverViewQueryHandler(IReadRepositoryManager readRepositoryManager, IMapper mapper, IOptions<DocumentSetting> documentSetting)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _documentSetting = documentSetting.Value;
        }

        public async Task<CustomResponse<List<OverViewDto>>> Handle(GetAllOverViewQuery request, CancellationToken cancellationToken)
        {
            var result = await _readRepositoryManager.OverViewRepository.GetOverViewListAsync();

            var respone = _mapper.Map<List<OverViewDto>>(result);

            return CustomResponse<List<OverViewDto>>.Success(respone, 200);
        }
    }
}