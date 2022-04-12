using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Settings;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.Sliders;

public class GetAllSliderQuery : IRequest<CustomResponse<List<SliderDto>>>
{
    public class GetAllSliderQueryHandler : IRequestHandler<GetAllSliderQuery, CustomResponse<List<SliderDto>>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly IMapper _mapper;
        private readonly DocumentSetting _documentSetting;

        public GetAllSliderQueryHandler(IReadRepositoryManager readRepositoryManager, IMapper mapper, IOptions<DocumentSetting> documentSetting)
        {
            _readRepositoryManager = readRepositoryManager;
            _mapper = mapper;
            _documentSetting = documentSetting.Value;
        }

        public async Task<CustomResponse<List<SliderDto>>> Handle(GetAllSliderQuery request, CancellationToken cancellationToken)
        {
            var result = await _readRepositoryManager.SliderRepository.GetSliderListAsync();

            var respone = _mapper.Map<List<SliderDto>>(result);

            respone.ForEach(x =>
            {
                x.ImageUrl = string.Concat(_documentSetting.StorageUrl, x.ImageUrl);
            });

            return CustomResponse<List<SliderDto>>.Success(respone, 200);
        }
    }
}