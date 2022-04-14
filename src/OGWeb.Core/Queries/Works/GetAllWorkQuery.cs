using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using OGWeb.Core.Dtos;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Settings;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Queries.Works;

public class GetAllWorkQuery : IRequest<CustomResponse<List<WorkListDto>>>
{

    public class GetAllWorkQueryHandler : IRequestHandler<GetAllWorkQuery, CustomResponse<List<WorkListDto>>>
    {
        private readonly IReadRepositoryManager _readRepositoryManager;
        private readonly DocumentSetting _documentSetting;

        public GetAllWorkQueryHandler(IReadRepositoryManager readRepositoryManager, IOptions<DocumentSetting> documentSetting)
        {
            _readRepositoryManager = readRepositoryManager;
            _documentSetting = documentSetting.Value;
        }

        public async Task<CustomResponse<List<WorkListDto>>> Handle(GetAllWorkQuery request, CancellationToken cancellationToken)
        {
            var result = await _readRepositoryManager.WorkRepository.GetWorkListAsync();

            var response = result.Select(x => new WorkListDto
            {
                Id = x.Id,
                SlugUrl = x.SlugUrl,
                Title = x.Title,
                Path = x.WorkFiles.FirstOrDefault().ImageUrl

            }).ToList();

            response.ForEach(x =>
            {
                x.ImageUrl = string.Concat(_documentSetting.StorageUrl, x.Path);
            });

            return CustomResponse<List<WorkListDto>>.Success(response, 200);
        }
    }
}