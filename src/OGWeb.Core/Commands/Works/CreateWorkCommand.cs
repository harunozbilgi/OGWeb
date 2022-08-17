using AutoMapper;
using MediaBalansDocument.Library;
using MediaBalansDocument.Library.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Dtos;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Works;

public class CreateWorkCommand : IRequest<CustomResponse<WorkDto>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keyword_Seo { get; set; }
    public string Description_Seo { get; set; }
    public bool? IsActived { get; set; }
    public List<IFormFile> Files { get; set; }

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
            string path = "files/works/";

            if (request == null)
            {
                _logger.LogError("eklemek istediginiz alanlar bos");

                throw new ArgumentNullException(nameof(request));
            }

            var work = _mapper.Map<Work>(request);
             
            var result = await _writeRepositoryManager.WorkRepository.CreateWorkAsync(work);

            if (request.Files != null && request.Files.Count > 0)
            {
                foreach (var item in request.Files)
                {
                    var response_file = await FileUploader.UploadAsync(item, path, cancellationToken);

                    string image_Url = string.Concat(path, response_file.DocumentName);

                    await _writeRepositoryManager.WorkRepository.CreateWorkFileAsync(new WorkFile() { WorkId = result.Id, ImageUrl = image_Url });
                   
                    await ImageHelper.OptimizeAsync(image_Url);
                }
            }

            var response = _mapper.Map<WorkDto>(result);

            return CustomResponse<WorkDto>.Success(response, 201);

        }
    }
}